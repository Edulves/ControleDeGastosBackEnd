using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.UserRequests;
using ExpensesControl.DTOs.Responses.UserReponses;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpensesControl.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ResultPattern<string>> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);

        if (userExists != null)
        {
            // Retorna um erro 400 que vai virar ProblemDetails graças à sua extensão
            return ResultPattern<string>.Failure(
                "Usuário com esse e-mail já existe.",
                "Erro de validação",
                StatusCodes.Status400BadRequest
            );
        }

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true // Em produção real, aqui enviaria o e-mail de confirmação
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            // Concatena os erros do Identity (ex: "Senha muito curta") em uma única string
            var errors = string.Join(" | ", result.Errors.Select(e => e.Description));

            return ResultPattern<string>.Failure(
                errors,
                "Erro ao criar usuário",
                StatusCodes.Status400BadRequest
            );
        }

        return ResultPattern<string>.Success("Usuário criado com sucesso!");
    }

    public async Task<ResultPattern<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return ResultPattern<LoginResponse>.Failure(
                "Credenciais inválidas.",
                "Não autorizado",
                StatusCodes.Status401Unauthorized
            );
        }

        var token = GenerateJwtToken(user);

        var response = new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };

        return ResultPattern<LoginResponse>.Success(response);
    }

    public async Task<ResultPattern<UserDataResponse>> GetCurrentUserAsync(ClaimsPrincipal userClaims)
    {
        // O ASP.NET já extraiu o usuário do Token JWT graças ao [Authorize] no Controller
        // Aqui nós só lemos os claims (dados) que colocamos dentro do token
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = userClaims.FindFirstValue(ClaimTypes.Email);

        if (userId == null)
        {
            return ResultPattern<UserDataResponse>.Failure(
                "Não foi possível extrair o ID do token.",
                "Token inválido",
                StatusCodes.Status401Unauthorized
            );
        }

        // Opcional: Se quiser garantir que o usuário ainda existe no banco:
        // var user = await _userManager.FindByIdAsync(userId);
        // if (user == null) return Failure(...);

        var response = new UserDataResponse
        {
            Id = userId,
            Email = userEmail ?? string.Empty
        };

        return ResultPattern<UserDataResponse>.Success(response);
    }

    // Método privado auxiliar para gerar o JWT
    private JwtSecurityToken GenerateJwtToken(IdentityUser user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), // Sub = Subject (ID do usuário)
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Jti = ID único do token
        };

        var minutes = Convert.ToDouble(jwtSettings["ExpiresInMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(minutes),
            signingCredentials: creds
        );

        return token;
    }
}