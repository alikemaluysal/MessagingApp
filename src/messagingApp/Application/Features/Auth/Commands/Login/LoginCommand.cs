using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string IpAddress  { get; set; }

    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: u => u.Email == request.Email);

            //business rule 1
            _authBusinessRules.UserShouldExist(user);

            //business rule 2
            _authBusinessRules.PasswordShouldMatch(user!, request.Password);

            var acccessToken = _authService.CreateAccessToken(user!);
            var refreshToken = await _authService.CreateRefreshTokenAsync(user!, request.IpAddress);

            return new LoggedResponse
            {
                AccessToken = acccessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }

}
