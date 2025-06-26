using EJERCICIOAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EJERCICIOAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Artista> Artistas { get; set; }
    }


}
