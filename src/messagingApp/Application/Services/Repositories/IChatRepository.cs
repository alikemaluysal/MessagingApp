using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IChatRepository : IAsyncRepository<Chat, Guid>, IRepository<Chat, Guid>
{
}
