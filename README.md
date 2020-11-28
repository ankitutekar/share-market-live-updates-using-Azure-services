# Share Market live updates using Azure services
<p>This is a small web-app implemented to get hands-on on Azure services. It provides live share market updates to connected clients. Note that, the share market updates are not consuming any real service, updates are simulated using custom update function written using Azure Functions and stored in Azure Cosmos DB. Also the shares are of made up companies!</p>
<h3>Technologies used:</h3>
<p>As this was developed while learning about serverless technologies, it heavily uses cloud services - 
<ul>
  <li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview">Azure Functions</a></li>
  <li><a href="https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-overview">Azure SignalR</a></li>
  <li><a href="https://docs.microsoft.com/en-us/azure/cosmos-db/introduction">Azure Cosmos DB</a></li>
  <li><a href="https://docs.microsoft.com/en-us/visualstudio/ide/quickstart-aspnet-core?view=vs-2019">Aspnetcore web application</a>, with AspNetCore.SpaServices extension for integrating Create-React-App</li>
  <li><a href="https://reactjs.org/">React JS</a></li>
</ul>
</p>
<h3>Flow diagram:</h3>
<img src="/ShareMarketLiveUpdatesFlowDiagram.jpg" alt="ShareMarketLiveUpdatesFlowDiagram" />
<br />
<p>So the main components are - 
<ul>
  <li>A client app accessed by users to get real time share updates</li>
  <li>Azure Function app having 4 different functions</li>
  <li>An Azure SignalR service to act as backplane for all client connections</li>
  <li>An Azure Cosmos DB instance which stores the share details</li>
</ul>
<br />
Process overview -
<ul>
  <li>Initially, client app loads shares data through <i>GetInitialPrices()</i> function which is listening to Http requests and returns the required data by fetching it from Cosmos DB.</li>
  <li>Once initial data has been received, client app initiates SignalR connection through 'negotiate' endpoint which is another Function - <i>GetConnection()</i>. This function authenticates to Azure SignalR service to obtain required credentials for connecting to it. This credentials are returned to client app using which it initiates connection to Azure SignalR service. This connection is WebSocket(or SSE/long polling, as per support by client browser).</li>
  <li>The <i>UpdateSimulator()</i> function uses timer trigger configured while defining it and updates share data in Cosmos DB. This update is communicated to <i>UpdateClient()</i> function through <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-cosmos-db-triggered-function">Cosmos DB trigger</a>.</li>
  <li>The <i>UpdateClient()</i> function then communicates this updates to Azure SignalR through output binding. Finally, SignalR communicates the updates to connected clients and the UI is updated.</li>
</ul>
</p>
<h3>Learnings have been documented on my blog - </h3>
  <ul>
    <li><a href="https://www.learningstuffwithankit.dev/making-sense-of-faas-by-learning-about-azure-functions-part-i">Making sense of FaaS by learning about Azure Functions – Part I</a></li>
    <li><a href="https://www.learningstuffwithankit.dev/making-sense-of-faas-by-learning-about-azure-functions-part-ii">Making sense of FaaS by learning about Azure Functions – Part II</a></li>
  </ul>
<h3>Want to setup locally and play with it? </h3>
<p>You will need to have basic familarity with dotnetcore development. This being a serverless app(except the client part, which can be hosted on Azure too!), you will need to create required Azure resources.
<ul>
  <li><a href="https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-quickstart-azure-functions-csharp">Here's an example of how to create Azure SignalR and use it.</a> After creating the resource you will get a connection string which you will need to put in <i>/ShareMarketLiveUpdates.Functions/local.settings.json</i> - <b>AzureSignalRConnectionString</b>.</li>
  <li><a href="https://docs.microsoft.com/en-us/azure/cosmos-db/create-cosmosdb-resources-portal">Here's a step by step guide on creating Azure Cosmos DB and adding data in it.</a> After creating the resource you will get a connection string which you will need to put in <i>/ShareMarketLiveUpdates.Functions/local.settings.json</i> - <b>cosmosforsharemarketupdates_DOCUMENTDB</b>.</li>
<li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function">Here's step by step guide for creating Azure Functions</a>. If you just want try these out locally, you can do it without creating Functions in Azure.</li>
</ul>
<i>ShareMarketLiveUpdates.Functions</i> is a function app project and <i>ShareMarketLiveUpdates.SignalRClient</i> is Aspnetcore web application project. Both of these are dotnet core 3.1 projects, make sure you have that installed on your system.
<ul>
  <li>Microsoft has provided tools for development of Azure Functions locally, you can run Functions through <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs">Visual Studio</a> or <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs">Visual Studio Code</a>. Azure Functions needs storage account when running in Azure, and <a href="https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator">storage emulator</a> while running locally. Make sure you have that running on your system.</li>
  <li>The client project can be ran through dotnet CLI/visual studio/visual studio code.</li>
</ul>
</p>
