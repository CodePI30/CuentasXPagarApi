using CuentasXPagar.data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CuentasXPagar.data.DbContextSqlServer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<LogOperacion> LogsOperaciones { get; set; }
        public DbSet<DocumentoPagar> DocumentoPago { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Concepto>().ToTable("conceptos");
            modelBuilder.Entity<Proveedor>().ToTable("proveedores");
            modelBuilder.Entity<DocumentoPagar>().ToTable("documentos_pagar");
            modelBuilder.Entity<LogOperacion>().ToTable("logs_operaciones");
        }

    }
}
