using Application.Features.Chats.Commands.Create;
using Application.Features.Chats.Commands.Join;
using Application.Features.Chats.Queries.GetById;
using Application.Features.Chats.Queries.GetByUserId;
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

        CreateMap<CreateChatCommand, Chat>();

        CreateMap<Chat, CreatedChatResponse>();

        CreateMap<Chat, JoinedChatResponse>();

        CreateMap<Chat, GetByIdChatResponse>().ForMember(dest => dest.ChatUsers, opt => opt.MapFrom(src => src.ChatUsers.Select(cu => new ChatUserDto
        {
            UserId = cu.UserId,
            Nickname = cu.User.Nickname
        })));


        CreateMap<Chat, GetByUserIdChatListItemDto>()
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().Content))

            .ForMember(dest => dest.LastMessageDate, opt => opt.MapFrom(src => src.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault().SentAt));

    }
}
