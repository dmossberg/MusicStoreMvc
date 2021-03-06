﻿using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using MvcMusicStore.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MvcMusicStore.Proxy
{
    public class RetryDelegatingHandler : DelegatingHandler
    {
        public RetryPolicy RetryPolicy { get; set; }

        public RetryDelegatingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            RetryPolicy = CustomRetryPolicyFactory.MakeHttpRetryPolicy();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;
            var currentRetryCount = 0;

            RetryPolicy.Retrying += (sender, args) =>
            {
                currentRetryCount = args.CurrentRetryCount;
            };

            try
            {
                await RetryPolicy.ExecuteAsync(async () =>
                {
                    responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    if ((int)responseMessage.StatusCode != 200)
                    {
                        Telemetry.LogServiceCallFailure(request.RequestUri.ToString(), currentRetryCount);
                        throw new HttpRequestExceptionWithStatus(string.Format("Response status code {0} indicates server error", (int)responseMessage.StatusCode))
                        {
                            StatusCode = responseMessage.StatusCode,
                            CurrentRetryCount = currentRetryCount
                        };
                    }

                    return responseMessage;

                }, cancellationToken).ConfigureAwait(false);

                return responseMessage;
            }
            catch (HttpRequestExceptionWithStatus exception)
            {
                if (exception.CurrentRetryCount >= 3)
                {
                    //write to log
                }

                if (responseMessage != null)
                {
                    return responseMessage;
                }

                throw;
            }
            catch (Exception)
            {
                if (responseMessage != null)
                {
                    return responseMessage;
                }

                throw;
            }
        }
    }


}