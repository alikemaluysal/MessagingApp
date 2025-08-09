function buildMessageHtml(msg, currentUserId, isGroup) {
    const isMine =  msg.senderId === currentUserId;
    const content = msg.content;
    const createdAt = msg.createdAt;
    const time = formatTime(createdAt);
    const senderName = msg.senderName ;
    const avatar = msg.senderImageUrl;

    console.log('is group: ', typeof(isGroup));
    console.log('is group: ', isGroup);

    return `
            <li class="chat-list ${isMine ? "right" : "left"}">
              <div class="conversation-list">
                <div class="chat-avatar ${isMine ? "d-none" : ""}"><img src="${avatar}" alt=""></div>
                <div class="user-chat-content">
                  <div class="ctext-wrap">
                    <div class="ctext-wrap-content"><p class="mb-0 ctext-content">${content}</p></div>
                    <div class="dropdown align-self-start message-box-drop d-none"></div>
                  </div>
                  <div class="conversation-name">
                    <span class="${isGroup && !isMine ? "" : "d-none"} name">${senderName}</span>
                    <small class="text-muted time">${time}</small>
                    <span class="text-success check-message-icon ${isMine ? "" : "d-none"}"><i class="bx bx-check"></i></span>
                  </div>
                </div>
              </div>
            </li>`;
}


function appendMessage(msg, currentUserId, isGroup) {

    console.log("append message")
    console.log(msg);

    var ul = document.getElementById("users-conversation");
    if (!ul) return;

    var html = buildMessageHtml(msg, currentUserId, isGroup);
    var tpl = document.createElement("template");
    tpl.innerHTML = html.trim();
    var li = tpl.content.firstElementChild;

    var p = li.querySelector(".ctext-content");
    var content = msg.content || msg.Content || "";
    if (p) p.textContent = content;

    ul.appendChild(li);

    if (window.ChatUI && typeof window.ChatUI.scrollToBottom === "function") {
        window.ChatUI.scrollToBottom();
    } else {
        var wrap = document.querySelector("#users-chat-conversation .simplebar-content-wrapper");
        if (wrap) wrap.scrollTop = wrap.scrollHeight;
    }
}

function formatTime(v) {
    try {
        var d = new Date(v);
        var h = d.getHours(), m = d.getMinutes();
        return (h < 10 ? "0" + h : h) + ":" + (m < 10 ? "0" + m : m);
    } catch { return ""; }
}

window.Chat = {
    buildMessageHtml,
    appendMessage,
};
