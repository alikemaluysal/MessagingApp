using Application.Repositories;
using Core.Persistence.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ChatParticipantRepository : EfRepositoryBase<ChatParticipant>, IChatParticipantRepository
{
    public ChatParticipantRepository(DbContext context) : base(context)
    {
    }
}
