using Application.Features.Messages.Queries.GetChatMessages;
using Application.Features.Messages.Rules;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommand : IRequest<MessageDto>
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;

    public class SendMessageCommandHandler(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        MessagesBusinessRules rules) : IRequestHandler<SendMessageCommand, MessageDto>
    {
        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {

            var user = await userRepository.GetAsync(u => u.Id == request.SenderId);

            await rules.CheckIfUserExistsAsync(request.SenderId);
            await rules.CheckIfChatExistsAsync(request.ChatId);



            var message = new Message
            {
                ChatId = request.ChatId,
                SenderId = request.SenderId,
                Content = request.Content,
                CreatedAt = DateTime.Now,
            };

            await messageRepository.AddAsync(message);

            var response = new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                IsSender = true,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                SenderName = user!.UserName,
                SenderImageUrl = user!.ProfileImageUrl
            };

            return response;

        }
    }

}



