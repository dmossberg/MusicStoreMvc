using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;

namespace MvcMusicStore.Proxy
{
    public static class CustomRetryPolicyFactory
    {
        public static RetryPolicy MakeHttpRetryPolicy()
        {
            var strategy = new HttpTransientErrorDetectionStrategy();
            return Exponential(strategy);
        }

        private static RetryPolicy Exponential(ITransientErrorDetectionStrategy strategy)
        {
            var retryCount = 3;
            var minBackoff = TimeSpan.FromSeconds(1);
            var maxBackoff = TimeSpan.FromSeconds(10);
            var deltaBackoff = TimeSpan.FromSeconds(5);

            var exponentialBackoff = new ExponentialBackoff(retryCount, minBackoff, maxBackoff, deltaBackoff);

            return new RetryPolicy(strategy, exponentialBackoff);
        }
    }


}