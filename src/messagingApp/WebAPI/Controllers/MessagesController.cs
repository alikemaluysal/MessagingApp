using Application.Features.Messages.Commands.SendMessage;
using Application.Features.Messages.Queries.GetListByChatId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("GetByChatId/{chatId}")]
    public async Task<IActionResult> GetMessageByChatId([FromRoute] Guid chatId)
    {
        var query = new GetListByChatIdMessageQuery { ChatId = chatId };
        var response = await mediator.Send(query);
        return Ok(response);
    }
}
