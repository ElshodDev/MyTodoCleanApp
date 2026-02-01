using MyTodoCleanApp.Domain.Entities;

namespace MyTodoCleanApp.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
