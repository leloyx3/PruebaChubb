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
                .WithMany(e => e.Productos)
                .HasForeignKey(e => e.Ubicacion)
                .IsRequired();
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
    }
}
