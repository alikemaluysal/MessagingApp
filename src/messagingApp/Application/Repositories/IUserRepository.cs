using Core.Persistence.Repository;
using Domain.Entities;


namespace Application.Repositories;

public interface IUserRepository : IQuery<User>, IAsyncRepository<User>, IRepository<User>
{
}

