using Application.Features.Chats.Constants;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Rules;

public class ChatsBusinessRules(IUserRepository userRepository, IChatRepository chatRepository)
{

    public async Task CheckIfUserExistsAsync(Guid userId)
    {
        if (!await userRepository.AnyAsync(c => c.Id == userId))
            throw new Exception(ChatMessages.UserNotFound);
    }

    public void CheckIfUserExists(User? user)
    {
        if (user is null)
            throw new Exception(ChatMessages.UserNotFound);
    }

    public void CheckIfUserIsNotSelf(Guid firstParticipantId, Guid secondParticipantId)
    {
        if (firstParticipantId == secondParticipantId)
            throw new Exception(ChatMessages.SelfDMError);
    }

    public async Task CheckIfChannelExistsAsync(Guid chatId)
    {
        if (!await chatRepository.AnyAsync(c => c.Id == chatId && c.IsGroup))
            throw new Exception(ChatMessages.ChatNotFound);
    }

    public void CheckIfChatExists(Chat? chat)
    {
        if (chat is null)
            throw new Exception(ChatMessages.ChatNotFound);
    }

}
