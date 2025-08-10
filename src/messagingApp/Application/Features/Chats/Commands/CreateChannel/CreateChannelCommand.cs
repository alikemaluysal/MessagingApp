using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Chats.Commands.CreateChannel;

public class CreateChannelCommand : IRequest<Guid>
{
    public string ChannelName { get; set; } = string.Empty;
    public Guid CreatedById { get; set; }

    public class Handler(IChatRepository chatRepository) : IRequestHandler<CreateChannelCommand, Guid>
    {
        public async Task<Guid> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Name = request.ChannelName,
                IsGroup = true,
                CreatedById = request.CreatedById,
                CreatedAt = DateTime.Now,
                Participants = new List<ChatParticipant>
                {
                    new ChatParticipant { UserId = request.CreatedById, IsAdmin= true }
                }
            };


            await chatRepository.AddAsync(chat);

            return chat.Id;
        }
    }
}
