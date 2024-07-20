using Core.Persistence.Repositories;
using Domain.Entities;

namespace Core.Application.Services.Repositories;

public interface IChatRepository : IAsyncRepository<Chat, Guid>, IRepository<Chat, Guid>
{
}
