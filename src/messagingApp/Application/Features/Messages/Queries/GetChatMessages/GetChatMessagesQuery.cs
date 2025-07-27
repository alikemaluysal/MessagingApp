using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<GetChatMessagesResponse>
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }

    public class GetChatMessagesHandler(IMessageRepository messagesRepository, IChatParticipantRepository chatParticipantRepository, IChatRepository chatRepository) : IRequestHandler<GetChatMessagesQuery, GetChatMessagesResponse>
    {
      
        public async Task<GetChatMessagesResponse> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await messagesRepository.Query()
                                .Include(m => m.Sender)
                                .Where(m => m.ChatId == request.ChatId)
                                .OrderBy(m => m.CreatedAt)
                                .Select(m => new MessageDto
                                {
                                    Id = m.Id,
                                    Content = m.Content,
                                    SenderId = m.SenderId,
                                    CreatedAt = m.CreatedAt,
                                    IsSender = m.SenderId == request.UserId,
                                    SenderName = m.Sender.UserName,
                                    SenderImageUrl = m.Sender.ProfileImageUrl
                                }).ToListAsync();


            var chatInfo = await chatRepository.Query()
                                .Where(c => c.Id == request.ChatId)
                                .Select(c => new ChatInfoDto
                                {
                                    UserId = request.UserId,
                                    ChatId = c.Id,
                                    Name = c.IsGroup ? c.Name :
                                    c.Participants.FirstOrDefault(c => c.UserId != request.UserId).User.DisplayName,
                                    ImageUrl = c.IsGroup ? c.GroupImageUrl :
                                     c.Participants.FirstOrDefault(c => c.UserId != request.UserId).User.ProfileImageUrl,
                                    IsGroup = c.IsGroup,
                                    ParticipantsCount = c.Participants.Count
                                }).FirstOrDefaultAsync();


            var response = new GetChatMessagesResponse
            {
                Messages = messages,
                ChatInfo = chatInfo
            };

            return response;
        }
    }
}
