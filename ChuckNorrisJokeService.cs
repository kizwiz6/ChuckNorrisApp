using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorrisApp
{
    public class ChuckNorrisJokeService : IJokeService
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisJokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomJokeAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://api.chucknorris.io/jokes/random");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic joke = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    return joke.value;
                }
                else
                {
                    throw new Exception($"Request failed with status code {response.StatusCode}");
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
