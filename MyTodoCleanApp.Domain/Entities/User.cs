using Microsoft.AspNetCore.Identity;

namespace MyTodoCleanApp.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}