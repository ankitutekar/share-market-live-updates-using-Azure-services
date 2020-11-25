using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ShareMarketLiveUpdates.Functions.UpdateHandler
{
    public class GetConnection
    {
        /**********************************************
         - HttpTrigger - Called from clients for 
           getting SignalR connection information
           through Http request
         - SignalR output binding - Authenticates with
           specified hub and returns received connection
           information to client
        **********************************************/
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetShareMarketSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "notifications")] SignalRConnectionInfo connectionInfo)
            {
                return connectionInfo;
            }
    }
}