using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Chats.Commands.CreateChat;

public class GetOrCreateDirectMessageChatCommand : IRequest<Guid>
{
    public Guid FirstParticipantId { get; set; }
    public string SecondParticipantName { get; set; }


    public class Handler(
        IChatRepository chatRepository,
        IUserRepository userRepository
        ) : IRequestHandler<GetOrCreateDirectMessageChatCommand, Guid>
    {
        public async Task<Guid> Handle(GetOrCreateDirectMessageChatCommand request, CancellationToken cancellationToken)
        {

            var secondUser = await userRepository.GetAsync(u => u.UserName == request.SecondParticipantName);


            //TODO: business rule classına taşı
            if (secondUser is null)
                throw new ArgumentException("Second participant not found.");

            if (request.FirstParticipantId == secondUser.Id)
                throw new ArgumentException("Participants must be different.");

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
