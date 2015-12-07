using MvcMusicStore.Proxy;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcMusicStore.ServiceProxy
{
    public class MusicStoreAPIClient
    {
        public static async Task<string> retrieveFromCache()
        {
            string cacheData;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MusicStore.API.Uri"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("api/cache");
                if (response.IsSuccessStatusCode)
                {
                    cacheData = await response.Content.ReadAsStringAsync();
                    return cacheData;
                }
                else
                {
                    throw new ServiceCallException(string.Format("call to backend failed with status = {0}", response.StatusCode), "api/cache");
                }
            }
        }
    }
}