using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ShareMarketLiveUpdates.Functions.UpdateHandler
{
    public class GetConnection
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetShareMarketSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "notifications")] SignalRConnectionInfo connectionInfo)
            {
                return connectionInfo;
            }
    }
}