using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ShareMarketLiveUpdates.Functions.Models;

namespace ShareMarketLiveUpdates.Functions.UpdateHandler
{
    public class GetInitialPrices
    {
         /**********************************************
         - HttpTrigger - Called from clients for 
           getting initial share prices, when page is 
           loaded
         - Cosmos DB input binding - Fetches shares data
           from specified CosmosDB connection
        **********************************************/
        [FunctionName("GetInitialPrices")]
         public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [CosmosDB( //input binding
            databaseName: "cosmos-for-share-market-updates",
            collectionName: "Shares",
            ConnectionStringSetting = "cosmosforsharemarketupdates_DOCUMENTDB")]IEnumerable<ShareInfo> shares,
            ILogger log)
        {
            return new OkObjectResult(shares);
        }
        
    }
}