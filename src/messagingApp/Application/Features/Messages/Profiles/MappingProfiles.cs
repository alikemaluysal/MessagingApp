using Application.Features.Messages.Commands.SendMessage;
using Application.Features.Messages.Queries.GetListByChatId;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<SendMessageCommand, Message>();
		CreateMap<Message, SentMessageResponse>();
		CreateMap<Message, GetListByChatIdMessageListItemDto>().ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.User.Nickname));
	}
}
