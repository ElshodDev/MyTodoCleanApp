using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTodoCleanApp.Application.TodoItems.Commands.CreateTodoItem;
using MyTodoCleanApp.Application.TodoItems.Commands.DeleteTodoItem;
using MyTodoCleanApp.Application.TodoItems.Commands.UpdateTodoItem;
using MyTodoCleanApp.Application.TodoItems.Queries.GetTodoItems;

namespace MyTodoCleanApp.Api.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<List<TodoItemDto>>> Get([FromQuery] Guid? userId)
        {
            return await _mediator.Send(new GetTodoItemsQuery(userId));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID-lar mos kelmadi");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTodoItemCommand(id));
            return NoContent();
        }
    }
}