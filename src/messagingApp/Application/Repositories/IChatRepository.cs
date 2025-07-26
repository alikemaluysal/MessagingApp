using Core.Persistence.Repository;
using Domain.Entities;


namespace Application.Repositories;

public interface IChatRepository : IQuery<Chat>, IAsyncRepository<Chat>, IRepository<Chat>
{
}

