using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<GetChatMessagesResponse>
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }

    public class GetChatMessagesHandler(
        IMessageRepository messagesRepository,
        IChatParticipantRepository chatParticipantRepository,
        IChatRepository chatRepository)
        : IRequestHandler<GetChatMessagesQuery, GetChatMessagesResponse>
    {
        public async Task<GetChatMessagesResponse> Handle(GetChatMessagesQuery request, CancellationToken ct)
        {
            var isMember = await chatParticipantRepository
                .Query()
                .AsNoTracking()
                .AnyAsync(cp => cp.ChatId == request.ChatId && cp.UserId == request.UserId, ct);

            if (!isMember)
                throw new Exception("Bu sohbete erişim izniniz yok."); // istersen özel exception

            var chatInfo = await chatRepository
                .Query()
                .AsNoTracking()
                .Where(c => c.Id == request.ChatId)
                .Select(c => new ChatInfoDto
                {
                    UserId = request.UserId,
                    ChatId = c.Id,
                    Name = c.IsGroup
                        ? c.Name
                        : c.Participants
                            .Where(p => p.UserId != request.UserId)
                            .Select(p => p.User.DisplayName)
                            .FirstOrDefault(),
                    ImageUrl = c.IsGroup
                        ? c.GroupImageUrl
                        : c.Participants
                            .Where(p => p.UserId != request.UserId)
                            .Select(p => p.User.ProfileImageUrl)
                            .FirstOrDefault(),
                    IsGroup = c.IsGroup,
                    ParticipantsCount = c.Participants.Count
                })
                .FirstOrDefaultAsync(ct);

            if (chatInfo is null)
                throw new Exception("Sohbet bulunamadı.");

            var messages = await messagesRepository
                .Query()
                .AsNoTracking()
                .Where(m => m.ChatId == request.ChatId)
                .OrderBy(m => m.CreatedAt)
                .ThenBy(m => m.Id)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SenderId = m.SenderId,
                    CreatedAt = m.CreatedAt,
                    IsSender = m.SenderId == request.UserId,
                    SenderName = m.Sender.UserName,
                    SenderImageUrl = m.Sender.ProfileImageUrl
                })
                .ToListAsync(ct);

            return new GetChatMessagesResponse
            {
                Messages = messages,
                ChatInfo = chatInfo
            };
        }
    }
}
