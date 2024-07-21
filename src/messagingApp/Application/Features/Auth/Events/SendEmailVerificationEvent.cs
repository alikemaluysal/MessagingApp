using Application.Features.Auth.Rules;
using Application.Services.Mail;
using Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Events;

internal class SendEmailVerificationEvent : INotification
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }


    internal class UserRegisteredEventHandler : INotificationHandler<SendEmailVerificationEvent>
    {
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public UserRegisteredEventHandler(IMailService mailService, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
        {
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
        }

        public async Task Handle(SendEmailVerificationEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Id == notification.UserId);

            _authBusinessRules.UserShouldExist(user);

            var verificationCode = Guid.NewGuid().ToString().Substring(0,6);
            user.VerificationCode = verificationCode;
            await _userRepository.UpdateAsync(user);

            await _mailService.SendEmailAsync(notification.Email, "Welcome to MessagingApp", $"<h1>Welcome {notification.Nickname} your verification code is: {verificationCode}</h1>");
        }
    }
}
