namespace MyTodoCleanApp.Application.Users.Common;

public record AuthResponse(
    Guid Id,
    string UserName,
    string Email,
    string Token);

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string UserName, string Email, string Password, string FullName);