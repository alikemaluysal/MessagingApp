using FluentValidation;

namespace Application.Features.Chats.Commands.Join;

public class JoinChatCommandValidator : AbstractValidator<JoinChatCommand>
{
    public JoinChatCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ChatId).NotEmpty();
    }
}

