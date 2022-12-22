using ChuckNorrisApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Create an HttpClient instance
        HttpClient httpClient = new HttpClient();

        // Create an instance of the ChuckNorrisJokeService
        IJokeService jokeService = new ChuckNorrisJokeService(httpClient);

        // Get a random joke and print it to the console
        string joke = await jokeService.GetRandomJokeAsync();
        Console.WriteLine(joke);
    }
}