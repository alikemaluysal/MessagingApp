using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Queries.GetList;

public class GetListChatQuery : IRequest<List<GetListChatQueryListItemDto>>
{
    class GetListChatQueryHandler(IChatRepository chatRepository, IMapper mapper) : IRequestHandler<GetListChatQuery, List<GetListChatQueryListItemDto>>
    {
        public async Task<List<GetListChatQueryListItemDto>> Handle(GetListChatQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetListAsync();



            return mapper.Map<List<GetListChatQueryListItemDto>>(chats);
        }
    }
}

