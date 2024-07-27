using Application.Features.Messages.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController(IMediator mediator) : ControllerBase
{
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }
}
