using Application.Features.Auth.Constants;
using Application.Features.Chats.Constants;
using Application.Services.Repositories;
using Core.Application.Security;
using Core.Exception.Types;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Rules;

public class MessageBusinessRules
{
    public void ChatShouldExistWhenSelected(Chat? chat)
    {
        if (chat is null)
            throw new BusinessException("Chat not found");
    }


    public void UserShouldExistWhenSelected(User? user)
    {
        if (user is null)
            throw new BusinessException("User not found");
    }

}
