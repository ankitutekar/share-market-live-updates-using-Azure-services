using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ShareMarketLiveUpdates.Functions.Models;

namespace ShareMarketLiveUpdates.Functions
{
    public static class CosmosDBUpdateSimulator
    {
        static string[] _companies = { "Cakes and Bakes Ltd.", "Joey's Pizza",
                                      "Charles Chips", "Coke and Burgers", "Freddy's Fries" };
        static Random _randomNumberGenerator = new Random();

        [FunctionName("CosmosDBUpdateSimulator")]
        public static void Run([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, //trigger
            [CosmosDB( //input binding
            databaseName: "cosmos-for-share-market-updates",
            collectionName: "Shares",
            ConnectionStringSetting = "cosmosforsharemarketupdates_DOCUMENTDB")]IEnumerable<ShareInfo> shares,
            [CosmosDB( //output binding
            databaseName: "cosmos-for-share-market-updates",
            collectionName: "Shares",
            ConnectionStringSetting = "cosmosforsharemarketupdates_DOCUMENTDB")] out ShareInfo document, ILogger log)
            {
                var nextCompanyToUpdate = GetNextCompanyToUpdate();
                document = shares.Where(x => x.Company == nextCompanyToUpdate).FirstOrDefault();

                document.Volume++;
                document.Ltp += GetRandomTradedPriceUpdate();

                log.LogInformation($"Updated company: {document.Company} \n Latest Traded Price: {document.Ltp}");
            }
        
        private static string GetNextCompanyToUpdate()
        {
            return _companies[_randomNumberGenerator.Next(0, 4)];
        }

        private static int GetRandomTradedPriceUpdate()
        {
            return _randomNumberGenerator.Next(-10, 10);
        }
    }
}
