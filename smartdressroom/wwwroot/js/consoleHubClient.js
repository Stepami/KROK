const connection = new signalR.HubConnectionBuilder()
    .withUrl("/console")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

connection.on("roomAdded", (roomNumber) => $('#room').html("Комната " + roomNumber));
connection.on("queryAdded", (query) => console.log(JSON.stringify(query)));

connection.start().then(function () {
    console.log("connected");
});