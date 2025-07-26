using Application.Repositories;
using Core.Persistence.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}
