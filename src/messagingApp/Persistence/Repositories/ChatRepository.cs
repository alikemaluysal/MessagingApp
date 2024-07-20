using Core.Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ChatRepository : EfRepositoryBase<Chat, Guid, AppDbContext>, IChatRepository
{
    public ChatRepository(AppDbContext context) : base(context)
    {
    }
}
