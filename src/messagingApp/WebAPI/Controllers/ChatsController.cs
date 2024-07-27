using Application.Features.Chats.Commands.Create;
using Application.Features.Chats.Commands.Join;
using Application.Features.Chats.Queries;
using Application.Features.Chats.Queries.GetById;
using Application.Features.Chats.Queries.GetByUserId;
using Application.Features.Chats.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateChatCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Join")]
    public async Task<IActionResult> Join([FromBody] JoinChatCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new GetListChatQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetByIdChatQuery() { ChatId = id});
        return Ok(response);
    }



    [HttpGet("GetByUserId/{userId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var response = await mediator.Send(new GetByUserIdChatQuery() { UserId = userId });
        return Ok(response);
    }
}
