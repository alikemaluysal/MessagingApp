using Application.Repositories;
using Core.Persistence.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class MessageRepository : EfRepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(DbContext context) : base(context)
    {
    }
}
