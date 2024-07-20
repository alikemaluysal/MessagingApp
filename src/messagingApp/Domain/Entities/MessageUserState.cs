using Core.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class MessageUserState : Entity<Guid>
{
    public Guid MessageId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    public virtual Message Message { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}