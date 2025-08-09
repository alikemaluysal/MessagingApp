document.addEventListener("DOMContentLoaded", async () => {

    var chat = document.getElementById("users-chat-conversation");
    const selectedChatId = chat.dataset.selectedChatId;
    const currentUserId = chat.dataset.currentUserId;
    const isGroup = chat.dataset.isGroup == "True";

    console.log("Selected chat ID:", selectedChatId);
    console.log("Current user ID:", currentUserId);
    console.log("isGroup:", isGroup);

    const conn = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();



    conn.on("ReceiveMessage", (message) => {
        console.log("--------- ReceiveMessage ------")
        console.log(message);
        console.log(currentUserId);
        console.log(isGroup);
        console.log("--------------")

        appendMessage(message, currentUserId, isGroup);
    });



   await conn.start();


    conn.invoke("JoinChat", selectedChatId);


});