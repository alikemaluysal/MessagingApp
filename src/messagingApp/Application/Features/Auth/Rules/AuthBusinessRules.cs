using Application.Features.Auth.Constants;
using Application.Repositories;
using Core.Application.Security;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules(IUserRepository userRepository)
{

    public void CheckIfUserExists(User? user)
    {
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı.");
    }
    public async Task CheckIfUserEmailUnique(string email)
    {
        if (await userRepository.AnyAsync(u => u.Email == email))
            throw new Exception(AuthMessages.EmailAlreadyExists);
    }


    public async Task CheckIfUserNameUnique(string userName)
    {
        if (await userRepository.AnyAsync(u => u.UserName == userName))
            throw new Exception(AuthMessages.UserNameAlreadyExists);
    }

    public void CheckIfUserPasswordValid(string password, byte[] hash, byte[] salt)
    {
        if (!HashingHelper.VerifyPassworHash(password, hash, salt))
        {
            throw new Exception(AuthMessages.InvalidPassword);
        }
    }
}
