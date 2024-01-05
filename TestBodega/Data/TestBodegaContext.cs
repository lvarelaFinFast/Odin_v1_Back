using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestBodega.Models;
using TestBodega.Models.Dto;

#nullable disable

namespace TestBodega.Data
{
    public partial class TestBodegaContext : DbContext
    {
        
        public TestBodegaContext(DbContextOptions<TestBodegaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<TiposDetalle> TiposDetalles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Conductores> Conductores { get; set; }
        public virtual DbSet<Archivos> Archivos { get; set; }
        public virtual DbSet<ArchivosVehiculos> ArchivosVehiculos { get; set; }
        public virtual DbSet<TipoArchivos> TipoArchivos { get; set; }
        public virtual DbSet<TiposDocumento> tiposDocumento { get; set; }
        public virtual DbSet<EstadoConductor> EstadoConductors { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<Propietario> Propietarios { get; set; }
        public virtual DbSet<VehiculosPropietarios> VehiculosPropietarios { get; set; }
        public virtual DbSet<EmpresaVinculante> EmpresasVinculantes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Contrato> Contratos { get; set; }
        public virtual DbSet<Extracto> Extractos { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }
        public virtual DbSet<DataReporteDTO> SPDataReporte { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=DESKTOP-8T6JHVS; Initial Catalog=TestBodega; user id=sa; password=Clave.1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<MovimientoInventario>(entity =>
            {
                entity.ToTable("MovimientoInventario");

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.MovimientoInventarios)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Movimient__IdPro__36B12243");

                entity.HasOne(d => d.TipoMovimientoNavigation)
                    .WithMany(p => p.MovimientoInventarios)
                    .HasForeignKey(d => d.TipoMovimiento)
                    .HasConstraintName("FK__Movimient__TipoM__3D5E1FD2");
            });

            modelBuilder.Entity<Producto>(entity =>
            {

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TiposDetalle>(entity =>
            {
                entity.ToTable("TiposDetalle");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Conductores>(entity =>
            {
                entity.ToTable("Conductores");

            });
            modelBuilder.Entity<Archivos>(entity =>
            {
                entity.ToTable("Archivos");

            });
            modelBuilder.Entity<TipoArchivos>(entity =>
            {
                entity.ToTable("TipoArchivos");

            });
            modelBuilder.Entity<TiposDocumento>(entity =>
            {
                entity.ToTable("TiposDocumento");

            });
            modelBuilder.Entity<EstadoConductor>(entity =>
            {
                entity.ToTable("EstadoConductor");

            });
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculos");

            });
            modelBuilder.Entity<Propietario>(entity =>
            {
                entity.ToTable("Propietarios");

            });
            modelBuilder.Entity<VehiculosPropietarios>(entity =>
            {
                entity.ToTable("VehiculosPropietarios");

            });
            modelBuilder.Entity<EmpresaVinculante>(entity =>
            {
                entity.ToTable("EmpresaVinculante");

            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");

            });
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contratos");

            });
            modelBuilder.Entity<Extracto>(entity =>
            {
                entity.ToTable("Extracto");

            });
            modelBuilder.Entity<Municipios>(entity =>
            {
                entity.ToTable("Municipios");

            });

            modelBuilder.Entity<DataReporteDTO>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
