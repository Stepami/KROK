const hub = new signalR.HubConnectionBuilder()
    .withUrl("/mirrorhub")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

hub.on("onRoomAdded", (roomNumber) => $('#room').html("Комната " + roomNumber));

hub.onclose((error) => {
    console.log(error);
    setTimeout(() => {
        hub.start().then(() => {
            console.log("connected");
            hub.invoke('onRoomInitialized');
        });
    }, 5000); // Restart connection after 5 seconds.
});

hub.start().then(() => {
    console.log("connected");
    hub.invoke('onRoomInitialized');
});