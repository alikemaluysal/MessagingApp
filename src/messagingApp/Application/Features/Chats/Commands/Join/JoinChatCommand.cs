using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Commands.Join;

public class JoinChatCommand  : IRequest<JoinedChatResponse>
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }


    class JoinChatCommandHandler(
        IChatRepository chatRepository,
        IChatUserRepository chatUserRepository,
        IUserRepository userRepository,
        ChatBusinessRules chatBusinessRules,
        IMapper mapper
        
        ) : IRequestHandler<JoinChatCommand, JoinedChatResponse>
    {
        public async Task<JoinedChatResponse> Handle(JoinChatCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.Id == request.UserId);
            chatBusinessRules.UserShouldExistWhenSelected(user);

            var chat = await chatRepository.GetAsync(c => c.Id == request.ChatId, include: i => i.Include(c => c.ChatUsers));
            chatBusinessRules.ChatShouldExistWhenSelected(chat);

            var chatUser = chatUserRepository.Get(cu => cu.ChatId == chat!.Id && cu.UserId == user!.Id);
            chatBusinessRules.UserShouldNotBeInChatWhenJoining(chatUser);

            chatBusinessRules.CheckUserLimitWhenJoining(chat);

            ChatUser createdChatUser = new()
            {
                ChatId = chat!.Id,
                UserId = user!.Id
            };

            await chatUserRepository.AddAsync(createdChatUser);

            var respone = mapper.Map<JoinedChatResponse>(chat);

            return respone;
        }
    }
}

