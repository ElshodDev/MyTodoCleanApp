using MediatR;
using MyTodoCleanApp.Application.Common.Interfaces;

namespace MyTodoCleanApp.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity != null)
        {
            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}