using ControleDeGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoriasDeLancamentos> categorias_de_lancamentos { get; set; }
        public DbSet<GastosDiarios> gastos_diarios { get; set; }
    }
}
