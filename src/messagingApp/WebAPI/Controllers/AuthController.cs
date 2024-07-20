using Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
