using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTodoCleanApp.Application.Users.Commands.CreateUser;

namespace MyTodoCleanApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;
    public UsersController(ISender mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }
}