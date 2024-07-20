using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ChatUserRepository : EfRepositoryBase<ChatUser, Guid, AppDbContext>, IChatUserRepository
{
    public ChatUserRepository(AppDbContext context) : base(context)
    {
    }
}
