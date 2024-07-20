using Core.Persistence.Repositories;
using Domain.Entities;

namespace Core.Application.Services.Repositories;

public interface IChatUserRepository : IAsyncRepository<ChatUser, Guid>, IRepository<ChatUser, Guid>
{
}
