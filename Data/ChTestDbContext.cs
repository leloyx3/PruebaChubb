using ChTestPro.Models.DbTable;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChTestPro.Data
{
    public class ChTestDbContext: DbContext
    {
        public ChTestDbContext(DbContextOptions<ChTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
    }
}
