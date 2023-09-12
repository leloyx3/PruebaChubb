using ChTestPro.Models.DbTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace ChTestPro.Data
{
    public class ChTestDbContext: DbContext
    {
        public ChTestDbContext(DbContextOptions<ChTestDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(e => e.Ubicaciones)
                .WithOne(e => e.Productos)
                .HasForeignKey<Ubicacion>(e => e.Id);
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
    }
}
