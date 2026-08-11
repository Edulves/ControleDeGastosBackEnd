namespace ExpensesControl.Service.ServiceInterfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
}
