using Application.Features.Chats.Rules;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Chats.Commands.GetOrCreateDirectMessageChat;

public class GetOrCreateDirectMessageChatCommand : IRequest<Guid>
{
    public Guid FirstParticipantId { get; set; }
    public string SecondParticipantName { get; set; }


    public class Handler(
        IChatRepository chatRepository,
        IUserRepository userRepository,
        ChatsBusinessRules rules
        ) : IRequestHandler<GetOrCreateDirectMessageChatCommand, Guid>
    {
        public async Task<Guid> Handle(GetOrCreateDirectMessageChatCommand request, CancellationToken cancellationToken)
        {

            var secondUser = await userRepository.GetAsync(u => u.UserName == request.SecondParticipantName);

            rules.CheckIfUserExists(secondUser); 
            rules.CheckIfUserIsNotSelf(request.FirstParticipantId, secondUser.Id);

            var existingChat = await chatRepository
                .Query()
                .FirstOrDefaultAsync(c => c.Participants.Any(p => p.UserId == request.FirstParticipantId) &&
                                     c.Participants.Any(p => p.UserId == secondUser.Id));

            if (existingChat != null)
                return existingChat.Id;

            var chat = new Chat
            {
                CreatedAt = DateTime.Now,
                IsGroup = false,
                Participants = new List<ChatParticipant>
                {
                    new ChatParticipant { UserId = request.FirstParticipantId },
                    new ChatParticipant { UserId = secondUser.Id }
                },
            };


            await chatRepository.AddAsync(chat);
            return chat.Id;
        }
    }
}
