using Application.Features.Chats.Constants;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Rules;

public class ChatsBusinessRules
{
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

}
