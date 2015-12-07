using MvcMusicStore.Proxy;
using MvcMusicStore.ServiceProxy;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class BackEndProxy
{
    public static async Task<string> getValue()
    {
        using (var client = CustomHttpClientFactory.CreateWithRetries())
        {
            client.BaseAddress = new Uri("http://musicstorebackend.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/backend").Result;
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new ServiceCallException(string.Format("call to backend failed with status = {0}", response.StatusCode), "api/backend");
                }
        }
        
    }

   

}