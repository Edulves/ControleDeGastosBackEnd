using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TransactionCategory> categorias_de_lancamentos { get; set; }
        public DbSet<DailyExpense> gastos_diarios { get; set; }
        public DbSet<FixedExpense> gastos_fixos { get; set; }
    }
}
