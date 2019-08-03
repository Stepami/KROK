const connection = new signalR.HubConnectionBuilder()
    .withUrl("/mirrorhub")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

connection.on("roomAdded", (roomNumber) => $('#room').html("Комната " + roomNumber));
connection.on("queryAdded", (query) => console.log(query));

connection.start().then(function () {
    console.log("connected");
    connection.invoke('OnRoomInitialized');
});