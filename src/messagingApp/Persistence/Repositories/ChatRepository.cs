using Application.Repositories;
using Core.Persistence.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ChatRepository : EfRepositoryBase<Chat>, IChatRepository
{
    public ChatRepository(DbContext context) : base(context)
    {
    }
}
