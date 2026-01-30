using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTodoCleanApp.Application.TodoItems.Commands.CreateTodoItem;

namespace MyTodoCleanApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ISender _mediator;

    public TodoItemsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTodoItemCommand command)
    {
        return await _mediator.Send(command);
    }
}