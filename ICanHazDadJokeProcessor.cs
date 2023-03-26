using System;
using System.Net.Http;
using System.Threading.Tasks;
using ICanHazDadJoke_Search.Model;
namespace ICanHazDadJoke_Search
{
    public class ICanHazDadJokeProcessor
    {
        public static async Task<ICanHazDadJokeItem> LoadRandomJoke()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress))
            {
                if (response.IsSuccessStatusCode)
                {
                    ICanHazDadJokeItem joke = await response.Content.ReadAsAsync<ICanHazDadJokeItem>();

                    return joke;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ICanHazDadJoke> SearchJokes(string searchTerm)
        {
            string url = string.Empty;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                url = $"{ ApiHelper.ApiClient.BaseAddress }/search?limit=30&term={ searchTerm }";
            }
            else
            {
                url = $"{ ApiHelper.ApiClient.BaseAddress }/search?limit=30";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ICanHazDadJoke jokes = await response.Content.ReadAsAsync<ICanHazDadJoke>();
                    return jokes;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
