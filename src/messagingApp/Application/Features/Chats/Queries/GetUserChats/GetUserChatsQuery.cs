using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Chats.Queries.GetUserChats;

public class GetUserChatsQuery : IRequest<GetUserChatsResponse>
{
    public Guid UserId { get; set; }

    public class GetUserChatsQueryHandler(IChatRepository chatRepository)
        : IRequestHandler<GetUserChatsQuery, GetUserChatsResponse>
    {
        public async Task<GetUserChatsResponse> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            var items = await chatRepository.Query()
                .AsNoTracking()
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .Select(c => new ChatSummaryDto
                {
                    ChatId = c.Id,
                    IsGroup = c.IsGroup,

                    Name = c.IsGroup
                        ? (c.Name ?? "İsimsiz Grup")
                        : (
                            c.Participants
                                .Where(p => p.UserId != userId)
                                .Select(p => p.User.DisplayName)
                                .FirstOrDefault()
                            ?? "Bilinmeyen Kullanıcı"
                          ),

                    ImageUrl = c.IsGroup
                        ? c.GroupImageUrl
                        : c.Participants
                            .Where(p => p.UserId != userId)
                            .Select(p => p.User.ProfileImageUrl)
                            .FirstOrDefault(),

                    LastMessageTime = c.Messages
                        .OrderByDescending(m => m.CreatedAt)
                        .Select(m => (DateTime?)m.CreatedAt)
                        .FirstOrDefault()
                })
                .OrderByDescending(x => x.LastMessageTime.HasValue)
                .ThenByDescending(x => x.LastMessageTime)
                .ToListAsync(cancellationToken);

            return new GetUserChatsResponse
            {
                DirectMessages = items.Where(x => !x.IsGroup).ToList(),
                Channels = items.Where(x => x.IsGroup).ToList()
            };
        }
    }
}
