using Application.Features.Auth.Constants;
using Application.Features.Messages.Constants;
using Application.Repositories;
using Core.Application.Security;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Rules;

public class MessagesBusinessRules(IUserRepository userRepository, IChatRepository chatRepository)
{

    public async Task CheckIfUserExistsAsync(Guid userId)
    {
        if (!await userRepository.AnyAsync(c => c.Id == userId))
            throw new Exception(MessagesMessages.UserNotFound);
    }

    public async Task CheckIfChatExistsAsync(Guid chatId)
    {
        if (!await chatRepository.AnyAsync(c => c.Id == chatId))
            throw new Exception(MessagesMessages.ChatNotFound);
    }

}
