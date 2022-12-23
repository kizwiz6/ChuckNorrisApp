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



        while (true)
        {
            // Wait for the user to press a key
            Console.ReadKey();

            // Get a random joke and print it to the console
            string joke = await jokeService.GetRandomJokeAsync();
            Console.WriteLine(joke);
        }


    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add the HttpClient and ChuckNorrisJokeService to the service collection
        services.AddHttpClient<IJokeService, ChuckNorrisJokeService>();
        services.AddTransient<IJokeService, ChuckNorrisJokeService>();
    }
}