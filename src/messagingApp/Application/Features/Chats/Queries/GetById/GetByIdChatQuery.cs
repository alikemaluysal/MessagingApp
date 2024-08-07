﻿using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Queries.GetById;

public class GetByIdChatQuery : IRequest<GetByIdChatResponse>
{
    public Guid ChatId { get; set; }
    class GetListChatQueryHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules) : IRequestHandler<GetByIdChatQuery, GetByIdChatResponse>
    {
        public async Task<GetByIdChatResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
        {

            var chat = await chatRepository.GetAsync(
                predicate: c => c.Id == request.ChatId,
                include: i => i.Include(c => c.ChatUsers).ThenInclude(c => c.User));

            chatBusinessRules.ChatShouldExistWhenSelected(chat);


            return mapper.Map<GetByIdChatResponse>(chat);
        }
    }
}

