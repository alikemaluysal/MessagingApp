using Core.Persistence.Repository;
using Domain.Entities;


namespace Application.Repositories;

public interface IChatParticipantRepository : IQuery<ChatParticipant>, IAsyncRepository<ChatParticipant>, IRepository<ChatParticipant>
{
}

