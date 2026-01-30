using Microsoft.EntityFrameworkCore;
using MyTodoCleanApp.Domain.Entities;

namespace MyTodoCleanApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}