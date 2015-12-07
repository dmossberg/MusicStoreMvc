using System;

namespace MvcMusicStore.Proxy
{
    public class ServiceCallException : Exception
    {
        public ServiceCallException(string message, string url)
            : base(message)
        {
            ServiceUrl = url;
        }

            public string ServiceUrl
            {
                get;private set;
            }
    }
}