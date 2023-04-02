using Microsoft.AspNetCore.Components.Routing;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiteOfSweetsApp.Models.Tools
{
    public class APIAccessClass
    {
        public static  async Task<T> GetData<T>(string url) where T : new()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage result = await client.GetAsync(url);
                var content = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
