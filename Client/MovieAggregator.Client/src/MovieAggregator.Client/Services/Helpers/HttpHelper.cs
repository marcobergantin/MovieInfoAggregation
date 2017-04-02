using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAggregator.Client.Services.Helpers
{
    public static class HttpHelper
    {
        public static async Task<T> GetFromApi<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }
    }
}
