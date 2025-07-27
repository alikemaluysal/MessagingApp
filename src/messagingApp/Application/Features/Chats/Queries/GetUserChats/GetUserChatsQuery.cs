using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Chats.Queries.GetUserChats;

public class GetUserChatsQuery : IRequest<GetUserChatsResponse>
{
    public Guid UserId { get; set; }

    public class GetUserChatsQueryHandler(IChatRepository chatRepository) : IRequestHandler<GetUserChatsQuery, GetUserChatsResponse>
    {
        public async Task<GetUserChatsResponse> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            //TODO: improve query
            var userChats = await chatRepository.Query()
                            .Where(c => c.Participants.Any(p => p.UserId == userId))
                            .Select(c => new
                            {
                                c.Id,
                                c.IsGroup,
                                Name = c.IsGroup ? c.Name :
                                    c.Participants.FirstOrDefault(c => c.UserId != userId).User.DisplayName,
                                ImageUrl = c.IsGroup ? c.GroupImageUrl :
                                     c.Participants.FirstOrDefault(c => c.UserId != userId).User.ProfileImageUrl,
                                LastMessageTime = c.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault().CreatedAt
                            }).OrderByDescending(c => c.LastMessageTime).AsNoTracking().ToListAsync();


            var directMessages = userChats.Where(c => !c.IsGroup)
                .Select(c => new ChatSummaryDto
                {
                    ChatId = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    IsGroup = c.IsGroup,
                    LastMessageTime = c.LastMessageTime
                }).ToList();

            var channels = userChats.Where(c => c.IsGroup)
                    .Select(c => new ChatSummaryDto
                    {
                        ChatId = c.Id,
                        Name = c.Name,
                        ImageUrl = c.ImageUrl,
                        IsGroup = c.IsGroup,
                        LastMessageTime = c.LastMessageTime
                    }).ToList();

            return new GetUserChatsResponse
            {
               DirectMessages = directMessages,
                Channels = channels
            };

        }
    }

}
