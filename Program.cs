using ChuckNorrisApp;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Chuck Norris joke generator! Press any key to get a joke.");
        Console.ForegroundColor = ConsoleColor.Red;

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

            // Determine the appropriate action based on the key that was pressed
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    joke = GetPreviousJoke(jokes, ref jokeIndex);
                    break;
                case ConsoleKey.RightArrow:
                    joke = GetNextJoke(jokes, ref jokeIndex);
                    break;
                case ConsoleKey.UpArrow:
                    joke = await GetNewJoke(jokeService, jokes);
                    jokeIndex = jokes.Count - 1;
                    break;
            }
        }
    }

    private static string GetPreviousJoke(List<string> jokes, ref int jokeIndex)
    {
        jokeIndex--;

        if (jokeIndex < 0)
        {
            jokeIndex = jokes.Count - 1;
        }

        return jokes[jokeIndex];
    }

    private static string GetNextJoke(List<string> jokes, ref int jokeIndex)
    {
        jokeIndex++;

        if (jokeIndex >= jokes.Count)
        {
            jokeIndex = 0;
        }

        return jokes[jokeIndex];
    }

    private static async Task<string> GetNewJoke(IJokeService jokeService, List<string> jokes)
    {
        string joke = await jokeService.GetRandomJokeAsync();
        jokes.Add(joke);
        return joke;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add the HttpClient and ChuckNorrisJokeService to the service collection
        services.AddHttpClient<IJokeService, ChuckNorrisJokeService>();
        services.AddTransient<IJokeService, ChuckNorrisJokeService>();
    }
}