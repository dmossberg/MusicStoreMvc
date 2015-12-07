using Microsoft.ApplicationInsights;
using System.Collections.Generic;

namespace MvcMusicStore.Logging
{
    public class Telemetry
    {

        public static void LogServiceCallFailure(string url, int attempt)
        {
            TelemetryClient telemetry = new TelemetryClient();

            var d = new Dictionary<string, string>();
            d.Add("service call failed", url);
            d.Add("attempt", attempt.ToString());
            telemetry.TrackEvent(url, d);
        }
    }
}