using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.VerifyEmail;

public class VerifyEmailCommand : IRequest<VerifyEmailResponse>
{
    public string Email { get; set; }
    public string Code { get; set; }


    class VerifyEmailCommandHandler(
        IUserRepository userRepository, 
        AuthBusinessRules authBusinessRules) : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
    {


        public async Task<VerifyEmailResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.Email == request.Email);

            authBusinessRules.UserShouldExist(user);

            authBusinessRules.VerificationCodeShouldBeCorrect(user!, request.Code);

            user!.IsVerified = true;
            await userRepository.UpdateAsync(user);

            return new VerifyEmailResponse
            {
                Message = "Email verified successfully"
            };
        }
    }

}

