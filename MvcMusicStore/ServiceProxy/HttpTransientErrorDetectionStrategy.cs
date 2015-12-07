using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;

namespace MvcMusicStore.Proxy
{
    public class HttpTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                HttpRequestExceptionWithStatus httpException;

                if ((httpException = ex as HttpRequestExceptionWithStatus) != null)
                {
                    //if (httpException.StatusCode == HttpStatusCode.ServiceUnavailable)
                    //{
                    //    return true;
                    //}

                    return true;
                }
            }

            return false;
        }
    }


}