using Application.Features.Auth.Rules;
using Application.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    public class RegisterCommandHandler(IUserRepository userRepository, AuthBusinessRules rules) : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await rules.CheckIfUserEmailUnique(request.Email);
            await rules.CheckIfUserNameUnique(request.UserName);


            var (hash, salt) = HashingHelper.CreatePasswordHash(request.Password);

            //TODO: automapper
            var user = new User
            {
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                Email = request.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
            };

            await userRepository.AddAsync(user);

            //TODO: Email verification


            //TODO: automapper
            return new RegisterCommandResponse
            {
                Id = user.Id,
            };

        }
    }

}

