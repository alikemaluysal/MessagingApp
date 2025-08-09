(function () {
    var assetsUser = "assets/images/users/user-dummy-img.jpg";
    var assetsMulti = "assets/images/users/multi-user.jpg";
    var state = { mode: "users", replyOpen: false, selfId: 1 };

    function $(sel, root) { return (root || document).querySelector(sel); }
    function $all(sel, root) { return Array.from((root || document).querySelectorAll(sel)); }

    function showPane() {
        var channel = $("#channel-chat");
        var users = $("#users-chat");
        if (state.mode === "users") {
            if (channel) channel.style.display = "none";
            if (users) users.style.display = "block";
        } else {
            if (channel) channel.style.display = "block";
            if (users) users.style.display = "none";
        }
    }

    function scrollToBottom(containerId) {
        var hostId = containerId || "users-chat";
        var host = document.getElementById(hostId);
        if (!host) return;
        var wrapper = host.querySelector("#users-chat-conversation .simplebar-content-wrapper") ||
            host.querySelector("#chat-conversation .simplebar-content-wrapper") ||
            host.querySelector(".simplebar-content-wrapper");
        var list = host.getElementsByClassName("chat-conversation-list")[0];
        var height = list ? list.scrollHeight : 0;
        if (wrapper) wrapper.scrollTo({ top: height, behavior: "smooth" });
    }

    function timeString() {
        var d = new Date();
        var ampm = d.getHours() >= 12 ? "pm" : "am";
        var h = d.getHours() > 12 ? d.getHours() % 12 : d.getHours();
        var m = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
        return (h < 10 ? "0" + h : h) + ":" + m + " " + ampm;
    }

    function openReplyFromElement(anchor) {
        var li = anchor.closest(".chat-list");
        if (!li) return;
        var text = "";
        var wrap = anchor.closest(".ctext-wrap");
        if (wrap && wrap.children[0] && wrap.children[0].children[0]) {
            text = wrap.children[0].children[0].innerText || "";
        }
        var name = "You";
        var isLeft = !li || li.classList.contains("left");
        if (isLeft) {
            var topU = $(".user-chat-topbar .text-truncate .username");
            name = topU ? topU.innerHTML : "";
        }
        var card = $(".replyCard");
        if (!card) return;
        card.classList.add("show");
        state.replyOpen = true;
        var cn = $(".replyCard .replymessage-block .flex-grow-1 .conversation-name");
        if (cn) cn.innerText = name;
        var ptxt = $(".replyCard .replymessage-block .flex-grow-1 .mb-0");
        if (ptxt) ptxt.innerText = text;
    }

    function openReply(name, text) {
        var card = $(".replyCard");
        if (!card) return;
        card.classList.add("show");
        state.replyOpen = true;
        var cn = $(".replyCard .replymessage-block .flex-grow-1 .conversation-name");
        if (cn) cn.innerText = name || "";
        var ptxt = $(".replyCard .replymessage-block .flex-grow-1 .mb-0");
        if (ptxt) ptxt.innerText = text || "";
    }

    function closeReply() {
        var card = $(".replyCard");
        if (!card) return;
        card.classList.remove("show");
        state.replyOpen = false;
    }

    function copyFromElement(anchor) {
        var wrap = anchor.closest(".ctext-wrap");
        var text = wrap && wrap.children[0] && wrap.children[0].children[0] ? wrap.children[0].children[0].innerText : "";
        if (!text) return;
        if (navigator.clipboard && navigator.clipboard.writeText) navigator.clipboard.writeText(text);
        var a1 = $("#copyClipBoard");
        var a2 = $("#copyClipBoardChannel");
        if (a1) { a1.style.display = "block"; setTimeout(function () { a1.style.display = "none"; }, 1000); }
        if (a2) { a2.style.display = "block"; setTimeout(function () { a2.style.display = "none"; }, 1000); }
    }

    function deleteMessage(target) {
        var el = typeof target === "string" ? document.querySelector(target) : target;
        if (!el) return false;
        var content = el.closest(".user-chat-content");
        if (content) {
            if (content.childElementCount === 2) {
                var li1 = el.closest(".chat-list");
                if (li1) { li1.remove(); return true; }
            } else {
                var cw = el.closest(".ctext-wrap");
                if (cw) { cw.remove(); return true; }
            }
        }
        var li2 = el.closest(".chat-list");
        if (li2) { li2.remove(); return true; }
        return false;
    }

    function guardForm() {
        var form = $("#chatinput-form");
        if (!form) return;
        var input = $("#chat-input");
        var fb = $(".chat-input-feedback");
        form.addEventListener("submit", function (e) {
            var val = input ? (input.value || "") : "";
            if (val.length === 0) {
                e.preventDefault();
                if (fb) {
                    fb.classList.add("show");
                    setTimeout(function () { fb.classList.remove("show"); }, 2000);
                }
            }
        });
    }

    function wireConversation(host) {
        if (!host) return;
        host.addEventListener("click", function (evt) {
            var el = evt.target instanceof Element ? evt.target.closest(".reply-message") : null;
            if (el) { evt.preventDefault(); openReplyFromElement(el); }
        });
        host.addEventListener("click", function (evt) {
            var el = evt.target instanceof Element ? evt.target.closest("#close_toggle") : null;
            if (el) { evt.preventDefault(); closeReply(); }
        });
        host.addEventListener("click", function (evt) {
            var el = evt.target instanceof Element ? evt.target.closest(".copy-message") : null;
            if (el) { evt.preventDefault(); copyFromElement(el); }
        });
        host.addEventListener("click", function (evt) {
            var el = evt.target instanceof Element ? evt.target.closest(".delete-item") : null;
            if (el) { evt.preventDefault(); deleteMessage(el); }
        });
    }

    function wireChannel(host) {
        if (!host) return;
        host.addEventListener("click", function (evt) {
            var el = evt.target instanceof Element ? evt.target.closest(".copy-message") : null;
            if (el) { evt.preventDefault(); copyFromElement(el); }
        });
    }

    function wireMobileBack() {
        $all(".user-chat-remove").forEach(function (btn) {
            btn.addEventListener("click", function (e) {
                e.preventDefault();
                $all(".user-chat").forEach(function (el) { el.classList.remove("user-chat-show"); });
            });
        });
    }

    function wireFavorite() {
        $all(".favourite-btn").forEach(function (btn) {
            btn.addEventListener("click", function () { btn.classList.toggle("active"); });
        });
    }

    function wireSearchInputs() {
        $all("#searchMessage").forEach(function (inp) {
            inp.addEventListener("keyup", function () { search(window.document.getElementById("searchMessage") ? window.document.getElementById("searchMessage").value : ""); });
            inp.addEventListener("input", function () { search(window.document.getElementById("searchMessage") ? window.document.getElementById("searchMessage").value : ""); });
        });
    }

    function search(q) {
        var query = (q || "").toUpperCase();
        var list = document.getElementById("users-conversation");
        if (!list) return;
        $all("li", list).forEach(function (li) {
            var p = li.querySelector(".ctext-content") || li.getElementsByTagName("p")[0];
            var text = p ? ((p.textContent || p.innerText) || "") : "";
            li.style.display = text.toUpperCase().indexOf(query) > -1 ? "" : "none";
        });
    }

    function wireDeleteImages() {
        var root = $(".chat-conversation-list");
        if (!root) return;
        root.querySelectorAll(".chat-conversation-list .chat-list").forEach(function (li) {
            li.querySelectorAll(".delete-image").forEach(function (btn) {
                btn.addEventListener("click", function () {
                    var msg = btn.closest(".message-img");
                    if (!msg) return;
                    if (msg.childElementCount === 1) {
                        var li2 = btn.closest(".chat-list");
                        if (li2) li2.remove();
                    } else {
                        var ml = btn.closest(".message-img-list");
                        if (ml) ml.remove();
                    }
                });
            });
        });
    }

    function wirePlugins() {
        if (typeof GLightbox === "function") GLightbox({ selector: ".popup-img", title: false });
        if (typeof FgEmojiPicker === "function") {
            new FgEmojiPicker({ trigger: [".emoji-btn"], removeOnSelection: false, closeButton: true, position: ["top", "right"], preFetch: true, dir: "assets/js/pages/plugins/json", insertInto: $(".chat-input") });
            var eb = $("#emoji-btn");
            if (eb) {
                eb.addEventListener("click", function () {
                    setTimeout(function () {
                        var t = document.getElementsByClassName("fg-emoji-picker")[0];
                        if (!t) return;
                        var left = window.getComputedStyle(t) ? window.getComputedStyle(t).getPropertyValue("left") : "";
                        if (!left) return;
                        left = left.replace("px", "");
                        t.style.left = (parseFloat(left) - 40) + "px";
                    }, 0);
                });
            }
        }
        var el = document.getElementById("chat-conversation");
        if (el && typeof SimpleBar === "function") {
            var sb = new SimpleBar(el);
            var sc = sb.getScrollElement();
            var ul = document.getElementById("users-conversation");
            if (sc && ul) sc.scrollTop = ul.scrollHeight;
        }
    }

    function setMode(mode) {
        state.mode = mode === "channel" ? "channel" : "users";
        showPane();
    }

    function refreshHandlers() {
        wireConversation($("#users-chat"));
        wireChannel($("#channel-conversation"));
        wireSearchInputs();
        wireDeleteImages();
        wireFavorite();
        scrollToBottom();
    }

    function init() {
        guardForm();
        wireConversation($("#users-chat"));
        wireChannel($("#channel-conversation"));
        wireMobileBack();
        wireSearchInputs();
        wireDeleteImages();
        wireFavorite();
        wirePlugins();
        showPane();
        scrollToBottom();
    }

    var ChatUI = {
        init: init,
        refresh: refreshHandlers,
        setMode: setMode,
        openReply: openReply,
        openReplyFromElement: openReplyFromElement,
        closeReply: closeReply,
        copyFromElement: copyFromElement,
        deleteMessage: deleteMessage,
        scrollToBottom: scrollToBottom,
        search: search,
        timeString: timeString,
        assets: { user: assetsUser, multi: assetsMulti }
    };

    window.ChatUI = ChatUI;
    window.searchMessages = function () { ChatUI.search(document.getElementById("searchMessage") ? document.getElementById("searchMessage").value : ""); };
    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", init); else init();
})();
