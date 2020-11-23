# Share Market live updates using Azure services
<p>This is a project implemented to get hands-on on Azure Services. It provides live share market updates to connected clients. Note that, the share market updates are not consuming any real service, updates are simulated using custom update function written using Azure Functions and stored in Azure Cosmos DB.</p>
<h3>Technologies used:</h3>
<p>As this was developed while learning about serverless technologies, it heavily uses serverless technolgies - 
<ul>
  <li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview">Azure Functions</a></li>
  <li><a href="https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-overview">Azure SignalR</a></li>
  <li><a href="https://docs.microsoft.com/en-us/azure/cosmos-db/introduction">Azure Cosmos DB</a></li>
  <li><a href="https://docs.microsoft.com/en-us/visualstudio/ide/quickstart-aspnet-core?view=vs-2019">Aspnetcore web application</a>, with AspNetCore.SpaServices extension for integrating Create-React-App</li>
  <li><a href="https://docs.microsoft.com/en-us/visualstudio/ide/quickstart-aspnet-core?view=vs-2019">React JS</a></li>
</ul>
</p>
<h3>Flow diagram:</h3>
<img src="/ShareMarketLiveUpdatesFlowDiagram.jpg" />
<br />
<p>So the main components are - 
<ul>
  <li>Azure Function app having 4 different functions</li>
  <li>A client app accessed by users to get real time share updates</li>
  <li>An Azure SignalR service to act as backplane for all client connections</li>
  <li>An Azure Cosmos DB instance which actually stores the share details</li>
</ul>
Initially, client app loads shares data through GetInitialPrices() function which is istening to Http requests and returns the required data to by fetching it from Cosmos DB. Once initial data has been received, client app initiates SignalR connection through 'negotiate' endpoint which is another Function -GetConnection(). This function authenticates to Azure SignalR service to obtain required credentials for connecting to it. This credentials are returned to client app using which it initiates connection to Azure SignalR service. This connection is WebSocket(or SSE/long polling, as per support by client browser).
The UpdateSimulator() function runs after configured time interval and updates share data in Cosmos DB. This update is communicated to UpdateClient() function through <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-cosmos-db-triggered-function">Cosmos DB trigger</a>. This UpdateClient() function then communicates this updates to Azure SignalR through output binding. Finally, SignalR communicates the updates to connected clients and the UI is updated.</a>
</p>
<h3>Learnings have been documented on my blog - </h3>
<h3>To setup locally -</h3>
<p>This being a serverless app(except the client part, which can be hosted on Azure too!), you will need to create required Azure resources.
<ul>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
</ul>
</p>
<p>Setup Azure Function app locally - </p>
<p>Setup client app locally - </p>

