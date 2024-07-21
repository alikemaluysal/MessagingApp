using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules(IUserRepository userRepository)
{
    public void UserShouldExist(User? user)
    {
        if (user is null)
            throw new Exception(ErrorMessages.UserNotFound);
    }

    public void PasswordShouldMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new Exception(ErrorMessages.InvalidPassword);
    }

    public async Task EmailShouldBeUnique(string email)
    {
        if (await userRepository.AnyAsync(u => u.Email == email))
            throw new Exception(ErrorMessages.EmailInUse);
    }

    public void RefreshTokenShouldExist(RefreshToken? token)
    {
        if (token is null)
            throw new Exception(ErrorMessages.UserNotFound);
    }

    public void RefreshTokenShouldBeActive(RefreshToken token)
    {
        if (token.Revoked is not null || token.ExpiresAt < DateTime.UtcNow)
            throw new Exception(ErrorMessages.InvalidToken);
    }

    public void IpAddressShouldMatch(RefreshToken token, string ipAddress)
    {
        if (token.CreatedByIp != ipAddress)
            throw new Exception(ErrorMessages.IpDoesNotMatch);
    }    
    
    public void VerificationCodeShouldBeCorrect(User user, string code)
    {
        if (user.VerificationCode is null || user.VerificationCode != code)
            throw new Exception(ErrorMessages.InvalidVerificationCode);
    }
}
