using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

//public class LoginCommandValidator : AbstractValidator<LoginCommand>
//{
//    public LoginCommandValidator()
//    {
//        RuleFor(x => x.Login.Email).NotEmpty().EmailAddress();
//        RuleFor(x => x.Login.Password).NotEmpty();
//    }
//}


public class LoginCommandValidator : AbstractValidator<UserForLoginDto>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
