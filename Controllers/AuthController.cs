using ExpensesControl.Data.ResultPattern.Extensions;
using ExpensesControl.DTOs.Requests.UserRequests;
using ExpensesControl.DTOs.Responses.UserReponses;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService; // Usando a injeção do Service como conversamos

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return (await _authService.RegisterAsync(request)).ToIActionResult(this); // Usa a sua extensão mágica!
    }

    /// <summary>
    /// Realiza login e retorna o Token JWT
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return (await _authService.LoginAsync(request)).ToIActionResult(this);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDataResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _authService.GetCurrentUserAsync(User);
        return result.ToIActionResult(this);
    }
}