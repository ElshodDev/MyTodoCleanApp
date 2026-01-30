using MediatR;
using MyTodoCleanApp.Application.Common.Interfaces;
using MyTodoCleanApp.Domain.Entities;

namespace MyTodoCleanApp.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email, string Password) : IRequest<Guid>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password,
            CreatedDate = DateTimeOffset.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}