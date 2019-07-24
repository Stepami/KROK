const connection = new signalR.HubConnectionBuilder()
    .withUrl("/console")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

connection.on("roomAdded", (roomNumber) => $('#room').html("Комната " + roomNumber));

connection.start().then(function () {
    console.log("connected");
});