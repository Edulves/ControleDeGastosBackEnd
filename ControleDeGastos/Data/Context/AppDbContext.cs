using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data.Contexto;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TransactionCategory> TransactionCategories { get; set; }
    public DbSet<DailyExpense> DailyExpenses { get; set; }
    public DbSet<FixedExpense> FixedExpenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
