using MediatR;
using MyTodoCleanApp.Application.Common.Interfaces;
using MyTodoCleanApp.Domain.Entities;

namespace MyTodoCleanApp.Application.TodoItems.Commands.CreateTodoItem
{
    public record CreateTodoItemCommand(string Title, string Note, Guid UserId) : IRequest<Guid>;

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Note = request.Note,
                UserId = request.UserId,
                CreatedDate = DateTimeOffset.UtcNow,
            };
            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
