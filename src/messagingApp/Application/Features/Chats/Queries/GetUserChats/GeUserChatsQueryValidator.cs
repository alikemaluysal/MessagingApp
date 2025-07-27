using FluentValidation;

namespace Application.Features.Chats.Queries.GetUserChats;

public class GeUserChatsQueryValidator : AbstractValidator<GetUserChatsQuery>
{
    public GeUserChatsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}