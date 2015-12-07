using MvcMusicStore.Proxy;
using System.Net.Http;

namespace MvcMusicStore.ServiceProxy
{
    public class CustomHttpClientFactory
    {
        public static HttpClient CreateWithRetries()
        {
            return new HttpClient(new RetryDelegatingHandler(new HttpClientHandler()), false);
        }

        public static HttpClient Default()
        {
            return new HttpClient();
        }
    }
}