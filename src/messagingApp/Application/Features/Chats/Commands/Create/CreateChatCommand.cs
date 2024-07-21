using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Commands.Create;

public class CreateChatCommand : IRequest<CreatedChatResponse>
{
    public string Name { get; set; } = default!;
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }


    class CreateChatCommandHandler(IChatRepository chatRepository, ChatBusinessRules chatBusinessRules, IMapper mapper) : IRequestHandler<CreateChatCommand, CreatedChatResponse>
    {
        public async Task<CreatedChatResponse> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = mapper.Map<Chat>(request);

            await chatRepository.AddAsync(chat);

            var response = mapper.Map<CreatedChatResponse>(chat);

            return response;

        }
    }
}
