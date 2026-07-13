using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TransactionCategories> categorias_de_lancamentos { get; set; }
        public DbSet<DailyExpenses> gastos_diarios { get; set; }
        public DbSet<FixedExpenseResult> gastos_fixos { get; set; }
    }
}
