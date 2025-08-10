using Application.Features.Chats.Rules;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Chats.Commands.JoinChannel;

public class JoinChannelCommand : IRequest<Guid>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }

    public class Handler(IChatParticipantRepository chatParticipantRepository, ChatsBusinessRules rules) : IRequestHandler<JoinChannelCommand, Guid>
    {
        public async Task<Guid> Handle(JoinChannelCommand request, CancellationToken cancellationToken)
        {
            await rules.CheckIfChannelExistsAsync(request.ChatId);
            await rules.CheckIfUserExistsAsync(request.UserId);

            var chatParticipantExists = await chatParticipantRepository.AnyAsync(x => x.ChatId == request.ChatId && x.UserId == request.UserId);

            if (chatParticipantExists)
                return request.ChatId;

            var chatParticipant = new ChatParticipant
            {
                ChatId = request.ChatId,
                UserId = request.UserId,
            };
            await chatParticipantRepository.AddAsync(chatParticipant);
            return request.ChatId;
        }
    }

}
