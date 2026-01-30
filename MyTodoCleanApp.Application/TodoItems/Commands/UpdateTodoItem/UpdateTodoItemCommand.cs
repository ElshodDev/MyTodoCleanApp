using MediatR;
using MyTodoCleanApp.Application.Common.Interfaces;

namespace MyTodoCleanApp.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand(Guid Id, string Title, string Note, bool IsDone) : IRequest;

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateTodoItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Vazifa topilmadi!");
        }

        entity.Title = request.Title;
        entity.Note = request.Note;
        entity.IsDone = request.IsDone;

        await _context.SaveChangesAsync(cancellationToken);
    }
}