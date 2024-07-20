using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules
{
    public void UserShouldExist(User? user)
    {
        if (user is null)
        {
            throw new Exception("User not found");
        }
    }

    public void PasswordShouldMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Invalid password");
        }
    }
}
