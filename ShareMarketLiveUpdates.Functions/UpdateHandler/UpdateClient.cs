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
        [FunctionName("UpdateClient")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "cosmos-for-share-market-updates",
            collectionName: "Shares",
            ConnectionStringSetting = "cosmosforsharemarketupdates_DOCUMENTDB",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input,
            [SignalR(HubName = "notifications")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
            {

                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
                var updatedShares = new List<ShareInfo>();
                foreach (var item in input)
                {
                   updatedShares.Add(JsonConvert.DeserializeObject<ShareInfo>(item.ToString())); 
                }
                await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "shareUpdated",
                    Arguments = new[] { updatedShares }
                });
            }
    }
}
