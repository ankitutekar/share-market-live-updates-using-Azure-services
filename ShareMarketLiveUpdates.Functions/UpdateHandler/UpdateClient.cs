using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Linq;
using Newtonsoft.Json;
using ShareMarketLiveUpdates.Functions.Models;

namespace ShareMarketLiveUpdates.Functions
{
    public static class UpdateClient
    {
        /**********************************************
         - CosmosDB Trigger - Function is triggered
           whenever any changes are made in specified
           Cosmos DB collection, receives updated 
           records in input parameter
         - SignalR output binding - Updated records are 
           returned to SignalR server, to be returned 
           to clients 
        **********************************************/
        [FunctionName("UpdateClient")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "cosmos-for-share-market-updates",
            collectionName: "Shares",
            ConnectionStringSetting = "cosmosforsharemarketupdates_DOCUMENTDB",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> updatedDocs,
            [SignalR(HubName = "notifications")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
            {

                log.LogInformation("Number of shares updated: " + updatedDocs.Count);
                log.LogInformation("First updated share Id: " + updatedDocs[0].Id);

                var updatedShares = new List<ShareInfo>();
                foreach (var item in updatedDocs)
                {
                   updatedShares.Add(JsonConvert.DeserializeObject<ShareInfo>(item.ToString())); 
                }

                await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "shareUpdated", //name of client function to be called
                    Arguments = new[] { updatedShares }
                });
            }
    }
}
