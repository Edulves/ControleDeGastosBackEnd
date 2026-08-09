namespace ExpensesControl.Models;

public abstract class Entity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}