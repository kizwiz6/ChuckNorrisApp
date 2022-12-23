using ChuckNorrisApp;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    static async Task Main(string[] args)
    {
        // Create a service collection and configure the dependencies
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Create a service provider from the service collection
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get the IJokeService instance from the service provider
        var jokeService = serviceProvider.GetRequiredService<IJokeService>();

        // Create a list to store the jokes
        var jokes = new List<string>();

        // Get the first joke
        string joke = await jokeService.GetRandomJokeAsync();
        jokes.Add(joke);

        // Set the current joke index to 0
        int jokeIndex = 0;

        while (true)
        {
            // Print the joke to the console
            Console.WriteLine(joke);

            // Wait for the user to press a key
            ConsoleKeyInfo key = Console.ReadKey();

            // If the user pressed the left arrow key, go to the previous joke
            if (key.Key == ConsoleKey.LeftArrow)
            {
                jokeIndex--;

                if (jokeIndex < 0)
                {
                    jokeIndex = jokes.Count - 1;
                }

                joke = jokes[jokeIndex];
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                jokeIndex++;

                if (jokeIndex >= jokes.Count)
                {
                    jokeIndex = 0;
                }

                joke = jokes[jokeIndex];
            }
            // If the user pressed the up arrow key, get a new joke
            else if (key.Key == ConsoleKey.UpArrow)
            {
                joke = await jokeService.GetRandomJokeAsync();
                jokes.Add(joke);
                jokeIndex = jokes.Count - 1;
            }
        }


    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add the HttpClient and ChuckNorrisJokeService to the service collection
        services.AddHttpClient<IJokeService, ChuckNorrisJokeService>();
        services.AddTransient<IJokeService, ChuckNorrisJokeService>();
    }
}