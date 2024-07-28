using Application.Features.Messages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommand : IRequest<SentMessageResponse>, ISecuredRequest
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public string? FileIdentifier { get; set; }

    public string[] Roles => [];

    class SendMessageCommandHandler(
        IUserRepository userRepository,
        IChatRepository chatRepository,
        IMessageUserStateRepository messageUserStateRepository,
        IMessageRepository messageRepository,
        MessageBusinessRules messageBusinessRules,
        IMapper mapper
        ) : IRequestHandler<SendMessageCommand, SentMessageResponse>
    {
        public async Task<SentMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var senderUser = await userRepository.GetAsync(c => c.Id == request.UserId);
            messageBusinessRules.UserShouldExistWhenSelected(senderUser);

            var chat = await chatRepository.GetAsync(
                predicate: c => c.Id == request.ChatId,
                include: i => i.Include(c => c.ChatUsers));

            messageBusinessRules.ChatShouldExistWhenSelected(chat);

            var message = mapper.Map<Message>(request);
            message.SentAt = DateTime.UtcNow;
            await messageRepository.AddAsync(message);


            var messageUserStates = chat.ChatUsers.Select(c => new MessageUserState
            {
                UserId = c.UserId,
                MessageId = message.Id,
                DeliveredAt = senderUser.Id == c.UserId ? DateTime.UtcNow : null,
                ReadAt = senderUser.Id == c.UserId ? DateTime.UtcNow : null
            }).ToList();

            await messageUserStateRepository.AddRangeAsync(messageUserStates);

            var response = mapper.Map<SentMessageResponse>(message);
            return response;
        }
    }

}
