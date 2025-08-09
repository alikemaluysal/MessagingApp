using Application.Features.Auth.Rules;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

//DTO
public class LoginCommand : IRequest<LoggedInCommandResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }


    public class LoginCommandHandler(IUserRepository userRepository, AuthBusinessRules rules) : IRequestHandler<LoginCommand, LoggedInCommandResponse>
    {
        public async Task<LoggedInCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.UserName == request.UserName);
            rules.CheckIfUserExists(user);
            rules.CheckIfUserPasswordValid(request.Password, user.PasswordHash, user.PasswordSalt);


            //TODO: automapper
            var response = new LoggedInCommandResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                IsVerified = user.IsVerified,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return response;
        }
    }
}

