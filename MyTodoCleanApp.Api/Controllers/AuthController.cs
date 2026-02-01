using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTodoCleanApp.Application.Common.Interfaces;
using MyTodoCleanApp.Application.Users.Common;
using MyTodoCleanApp.Domain.Entities;
using LoginRequest = MyTodoCleanApp.Application.Users.Common.LoginRequest;
using RegisterRequest = MyTodoCleanApp.Application.Users.Common.RegisterRequest;

namespace MyTodoCleanApp.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var token = _jwtTokenGenerator.GenerateToken(user);
        return Ok(new AuthResponse(
    user.Id,
    user.UserName ?? string.Empty,
    user.Email ?? string.Empty,
    token));
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized("Email yoki parol xato!");

        var token = _jwtTokenGenerator.GenerateToken(user);
        return Ok(new AuthResponse(user.Id, user.UserName, user.Email, token));
    }
}