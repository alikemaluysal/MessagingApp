document.addEventListener("DOMContentLoaded", async () => {

    var chat = document.getElementById("users-chat-conversation");
    const selectedChatId = chat.dataset.selectedChatId;
    const currentUserId = chat.dataset.currentUserId;
    const isGroup = chat.dataset.isGroup == "True";


    const conn = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    conn.on("ReceiveMessage", (message) => {
        appendMessage(message, currentUserId, isGroup);
    });

   await conn.start();
   conn.invoke("JoinChat", selectedChatId);

});