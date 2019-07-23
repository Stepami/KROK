var connection = new signalR.HubConnectionBuilder()
    .withUrl("/console")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

connection.start().then(function () {
    console.log("connected");
});