using FluentValidation;

namespace Application.Features.Chats.Commands.Create;

internal class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
{
    public CreateChatCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
