using Core.Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MessageRepository : EfRepositoryBase<Message, Guid, AppDbContext>, IMessageRepository
{
    public MessageRepository(AppDbContext context) : base(context)
    {
    }
}
