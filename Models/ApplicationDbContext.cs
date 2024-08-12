// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace ProductoService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .ToTable("categoria")
                .HasKey(c => c.CategoriaId);

            modelBuilder.Entity<Producto>()
                .ToTable("producto")
                .HasKey(p => p.ProductoId);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
