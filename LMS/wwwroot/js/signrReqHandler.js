﻿var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Admin/Chat')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message)
}