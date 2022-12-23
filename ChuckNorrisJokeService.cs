using ChuckNorrisJokes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChuckNorrisJokes
{
    public class ChuckNorrisJokeService : IJokeService
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _jokes;

        public ChuckNorrisJokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jokes = new List<string>();
        }

        public async Task<string> GetRandomJokeAsync()
        {
            try
            {
                // Check if there are any jokes in the cache
                if (_jokes.Count > 0)
                {
                    // Return a random joke from the cache
                    int index = new Random().Next(0, _jokes.Count);
                    string joke = _jokes[index];

                    // Clear the cache
                    _jokes.Clear();

                    return joke;
                }
                else
                {
                    // Make a GET request to the Chuck Norris joke API
                    HttpResponseMessage response = await _httpClient.GetAsync("https://api.chucknorris.io/jokes/random");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the joke from the response
                        string json = await response.Content.ReadAsStringAsync();
                        ChuckNorrisJoke joke = JsonConvert.DeserializeObject<ChuckNorrisJoke>(json);

                        // Add the joke to the cache
                        _jokes.Add(joke.Value);

                        // Return the joke text
                        return joke.Value;
                    }
                    else
                    {
                        // Return an error message if the request was not successful
                        return "An error occurred while retrieving the joke.";
                    }
                }
            }
            catch (Exception)
            {
                // Return an error message if an exception was thrown
                return "An error occurred while retrieving the joke.";
            }
        }
    }
}