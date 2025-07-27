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

public class SendMessageCommand : IRequest<Guid>
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;

    public class SendMessageCommandHandler(
        IMessageRepository messageRepository,
        MessagesBusinessRules rules) : IRequestHandler<SendMessageCommand, Guid>
    {
        public async Task<Guid> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
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
            return message.Id;

        }
    }

}



