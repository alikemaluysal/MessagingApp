using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

//DTO
public class LoginCommand : IRequest<LoggedInCommandResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedInCommandResponse>
    {
        public Task<LoggedInCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}

