namespace MyTodoCleanApp.Domain.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
