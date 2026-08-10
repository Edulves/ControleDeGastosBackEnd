using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.UserRequests;
using ExpensesControl.DTOs.Responses.UserReponses;
using System.Security.Claims;

namespace ExpensesControl.Service.ServiceInterfaces;

public interface IAuthService
{
    Task<ResultPattern<string>> RegisterAsync(RegisterRequest request);
    Task<ResultPattern<LoginResponse>> LoginAsync(LoginRequest request);
    Task<ResultPattern<UserDataResponse>> GetCurrentUserAsync(ClaimsPrincipal userClaims);
}
