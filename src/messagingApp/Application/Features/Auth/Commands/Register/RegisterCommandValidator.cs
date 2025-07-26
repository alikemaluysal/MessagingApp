using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
	public RegisterCommandValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Lütfen geçerli bir email adresi girin");
		RuleFor(x => x.UserName).NotEmpty();
		RuleFor(x => x.Password).NotEmpty();
		RuleFor(x => x.DisplayName).NotEmpty();
	}
}
