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

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto Register { get; set; }
    public string IpAddress { get; set; }


    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(IAuthService authService, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.EmailShouldBeUnique(request.Register.Email);

            HashingHelper.CreatePasswordHash(request.Register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Email = request.Register.Email,
                Nickname = request.Register.Nickname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _userRepository.AddAsync(user);
            var acccessToken = _authService.CreateAccessToken(user);
            var refreshToken = await _authService.CreateRefreshTokenAsync(user, request.IpAddress);

            return new RegisteredResponse
            {
                AccessToken = acccessToken,
                RefreshToken = refreshToken.Token
            };

        }
    }
}
