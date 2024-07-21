using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Refresh;

public class RefreshTokenCommand : IRequest<RefreshedTokenResponse>
{
    public string Token { get; set; }
    public string IpAddress { get; set; }


    class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(IAuthService authService, IRefreshTokenRepository refreshTokenRepository, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
        {
            _authService = authService;
            _refreshTokenRepository = refreshTokenRepository;
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
        }

        public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(t => t.Token == request.Token);

            //1. Check if the token exists
            _authBusinessRules.RefreshTokenShouldExist(refreshToken);
            //2. Check if the token is expired
            _authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

            //3. Check if ip address match
            _authBusinessRules.IpAddressShouldMatch(refreshToken!, request.IpAddress);


            var user = await _userRepository.GetAsync(u => u.Id == refreshToken!.UserId);

            //4. Check if user exists
            _authBusinessRules.UserShouldExist(user);

            await _authService.DeleteOldRefreshTokens(user!.Id);

            var newRefreshToken = await _authService.RotateRefreshToken(user!, refreshToken!, request.IpAddress);
            var accessToken = _authService.CreateAccessToken(user!);

            return new RefreshedTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
            };

        }
    }
}
