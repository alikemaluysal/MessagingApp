using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string UserName { get; set; }
    public IFormFile? ProfilePicture { get; set; }


    public class Handler(
        IUserRepository userRepository, 
        AuthBusinessRules rules,
        IFileService fileService) : IRequestHandler<UpdateProfileCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u => u.Id == request.UserId);
            rules.CheckIfUserExists(user);
            await rules.CheckIfUserNameUniqueWhenUpdated(request.UserName, user.Id);

            user.UserName = request.UserName;
            user.DisplayName = request.DisplayName;


            if (request.ProfilePicture is not null)
            {
                var result = await fileService.UploadFileAsync(request.ProfilePicture);
                user.ProfileImageUrl = result.FileUrl;
            }


            await userRepository.UpdateAsync(user);
            return user.Id;
        }
    }
}