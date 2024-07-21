using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Rules;

public class ChatBusinessRules(IChatRepository chatRepository)
{
    public void ChatShouldExistWhenSelected(Chat? user)
    {
        if (user is null)
            throw new Exception("Chat not found");
    }


}
