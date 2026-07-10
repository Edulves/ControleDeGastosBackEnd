using ControleDeGastos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EntryCategories> categorias_de_lancamentos { get; set; }
        public DbSet<DailyExpenses> gastos_diarios { get; set; }
        public DbSet<FixedExpenseResult> gastos_fixos { get; set; }
    }
}
