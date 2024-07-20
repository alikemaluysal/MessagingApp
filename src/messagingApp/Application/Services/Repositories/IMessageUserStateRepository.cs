using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;


public interface IMessageUserStateRepository : IAsyncRepository<MessageUserState, Guid>, IRepository<MessageUserState, Guid>
{
}
