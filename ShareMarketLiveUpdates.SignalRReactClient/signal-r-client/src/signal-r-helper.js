import {
    JsonHubProtocol,
    HubConnectionState,
    HubConnectionBuilder,
    LogLevel
  } from '@microsoft/signalr';
   
export const setupSignalR = async (connection, onSharesUpdated) => {

    connection = new HubConnectionBuilder()
      .withUrl(`${process.env.REACT_APP_FUNCTIONAPP_BASE_URL}`)
      .withAutomaticReconnect()
      .withHubProtocol(new JsonHubProtocol())
      .configureLogging(LogLevel.Information)
      .build();

    connection.serverTimeoutInMilliseconds = 60000;

  connection.onclose(error => {
    console.assert(connection.state === HubConnectionState.Disconnected);
    console.log('Connection closed!!! Please refresh this page to re-attempt the connection.', error);
  });

  connection.onreconnecting(error => {
    console.assert(connection.state === HubConnectionState.Reconnecting);
    console.log('Trying re-connect....', error);
  });

  connection.onreconnected(connectionId => {
    console.assert(connection.state === HubConnectionState.Connected);
    console.log('Connection successfully reestablished!! Connection id -> ', connectionId);
  });

  try {
   await connection.start();
    console.assert(connection.state === HubConnectionState.Connected);
    console.log('Connected to SignalR instance...');

    connection.on('shareUpdated', res => {
      onSharesUpdated(res);
      console.log("Share updated!, ", res);
    });

  } catch (err) {
    console.assert(connection.state === HubConnectionState.Disconnected);
    console.error('Connection Error: ', err);
  }
};
