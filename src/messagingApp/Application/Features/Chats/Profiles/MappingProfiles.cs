using Application.Features.Chats.Commands.Create;
using Application.Features.Chats.Queries;
using Application.Features.Chats.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Profiles;

internal class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Chat, GetListChatQueryListItemDto>();
		CreateMap<Chat, GetByIdChatResponse>();

		CreateMap<CreateChatCommand, Chat>();
		CreateMap<Chat, CreatedChatResponse>();
	}
}
