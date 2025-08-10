using Application.Features.Auth.Rules;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Queries.GetUser;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public Guid UserId { get; set; }

    public class Handler(IUserRepository userRepository, AuthBusinessRules rules) : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.Id == request.UserId);
            rules.CheckIfUserExists(user);

            return new GetUserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
                CreatedAt = user.CreatedAt,
                IsVerified = user.IsVerified,
                Email = user.Email,
            };
        }
    }
}
