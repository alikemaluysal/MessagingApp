//const Preloader = {
//    getElement: () => document.getElementById('Preloader'),
//    show: () => {
//        const preloader = Preloader.getElement();
//        preloader.style.display = 'flex';
//    },
//    hide: () => {
//        const preloader = Preloader.getElement();
//        preloader.style.display = 'none';
//    }
//};
//$(function () {
//    $(document).on("click", ".modal-link", evt => {
//        evt.preventDefault();
//        const url = $(evt.target).attr("href");
//        console.log({ evt, url });
//        fetch(url, {
//            method: 'GET',
//            headers: {
//                'X-Requested-With': 'fetch'
//            }
//        })
//            .then(resp => {
//                if (resp.ok) {
//                    return resp.text();
//                }
//                throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//            })
//            .then(resp => {
//                const modalContainer = document.getElementById('ModalContainer');
//                modalContainer
//                    .querySelector('.modal-body')
//                    .innerHTML = resp;
//                let myModal = bootstrap.Modal.getOrCreateInstance(modalContainer)
//                myModal.toggle(modalContainer);
//                //console.log({ resp, modalContainer, myModal });
//            });
//    }).on("submit", "form#FrmCreateGroup, form#FrmJoinGroup", async evt => {
//        evt.preventDefault();
//        const form = evt.target;

//        const url = form.action;
//        const method = form.method;

//        const data = new FormData(form);
//        form.querySelectorAll("input, button, textarea, select").forEach(el => {
//            el.disabled = true;
//        });

//        await fetch(url, {
//            method,
//            body: data,
//        })
//            .then(async resp => {
//                console.log(resp);
//                if (!resp.ok) {
//                    throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//                }

//                await fetch("/chat/")
//                    .then(resp => {
//                        if (resp.ok) {
//                            return resp.text();
//                        }
//                        throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//                    })
//                    .then(resp => {
//                        const chatContainer = document.getElementById('ContactListContainer');
//                        chatContainer.innerHTML = resp;
//                    });
//            })
//            .catch(err => {
//                console.log(err);
//            });
//    });

//    document.onclick = async evt => {
//        if (evt.target.matches("ul.contact-list > li, ul.contact-list > li *")) {
//            // TODO: show chat details and load chat messages to the relevant panel
//            Preloader.show();
//            const li = evt.target.closest("li");
//            const chatId = li.getAttribute("data-chat-id");

//            const activeLi = document.querySelector("ul.contact-list > li.active");
//            if (activeLi) {
//                activeLi.classList.remove("active");
//            }
//            li.classList.add("active");

//            const url = `/chat/${chatId}`;
//            const chatInfoTask = fetch(url, {
//                method: 'GET',
//                headers: {
//                    'X-Requested-With': 'fetch'
//                }
//            })
//                .then(resp => {
//                    if (resp.ok) {
//                        return resp.text();
//                    }
//                    throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//                })
//                .then(resp => {
//                    const chatContainer = document.getElementById('pnlChatInfo');
//                    chatContainer.innerHTML = resp;
//                    console.log({ resp, chatContainer });
//                });

//            const chatMessagesTask = fetch(`/chat/${chatId}/messages`)
//                .then(resp => {
//                    if (!resp.ok) {
//                        throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//                    }
//                    return resp.text();
//                }).then(html => {
//                    const chatMessagesContainer = document.getElementById('pnlMessageHistory');
//                    chatMessagesContainer.innerHTML = html;
//                });

//            await Promise.all([chatInfoTask, chatMessagesTask])
//                .catch(err => {
//                    console.log(err);
//                });

//            Preloader.hide();
//        }
//        if (evt.target.matches("#pnlChatInfo *")) {
//            const activeChatId = document.querySelector("ul.contact-list > li.active")?.getAttribute("data-chat-id");
//            if (!activeChatId) {
//                return;
//            }

//            await fetch(`/chat/${activeChatId}/details`)
//                .then(resp => {
//                    if (!resp.ok) {
//                        throw new Error(`Error: ${resp.status} - ${resp.statusText}`);
//                    }

//                    return resp.text();
//                })
//                .then(html => {
//                    const modalContainer = document.getElementById('ModalContainer');
//                    modalContainer
//                        .querySelector('.modal-body')
//                        .innerHTML = html;
//                    let myModal = bootstrap.Modal.getOrCreateInstance(modalContainer)
//                    myModal.toggle(modalContainer);
//                    //console.log({ resp, modalContainer, myModal });
//                })
//                .catch(err => {
//                    console.log(err);
//                })
//        }
//    };
//})