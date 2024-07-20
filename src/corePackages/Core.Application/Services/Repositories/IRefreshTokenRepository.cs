using Core.Persistence.Repositories;
using Domain.Entities;

namespace Core.Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, Guid>, IRepository<RefreshToken, Guid>
{
}
