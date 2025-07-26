using Core.Persistence.Repository;
using Domain.Entities;


namespace Application.Repositories;

public interface IMessageRepository : IQuery<Message>, IAsyncRepository<Message>, IRepository<Message>
{
}

