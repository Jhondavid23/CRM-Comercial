using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.Model;

namespace SistemaComercial.Model;

public partial class CvscDbcomercialContext : DbContext
{
    public CvscDbcomercialContext()
    {
    }

    public CvscDbcomercialContext(DbContextOptions<CvscDbcomercialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaInventario> Categoriainventarios { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<DetalleCotizacion> Detallecotizacions { get; set; }

    public virtual DbSet<DetalleCotizacionInventario> Detallecotizacioninventarios { get; set; }

    public virtual DbSet<DetalleCotizacionVariable> Detallecotizacionvariables { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> Menurols { get; set; }

    public virtual DbSet<Motivo> Motivos { get; set; }

    public virtual DbSet<NumeroDocumento> Numerodocumentos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    //public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }

    public virtual DbSet<TipoServicio> Tiposervicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VariablesEconomicas> Variableseconomicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=162.240.235.88;database=cvsc_dbcomercial;uid=cvsc_remote;password=Macs100607**", ServerVersion.Parse("5.7.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder
        //    .UseCollation("latin1_swedish_ci")
        //    .HasCharSet("latin1");

        modelBuilder.Entity<CategoriaInventario>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity
                .ToTable("categoriainventario")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdCategoria)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity
                .ToTable("cliente")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.CreatedBy, "FK__Cliente__created__534D60F1");

            entity.HasIndex(e => e.UptadtedBy, "FK__Cliente__uptadte__5441852A");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("createdBy");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nit)
                .HasColumnType("int(11)")
                .HasColumnName("nit");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .HasColumnName("nombreContacto");
            entity.Property(e => e.Prospecto)
                .HasDefaultValueSql("'1'")
                .HasColumnName("prospecto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");
            entity.Property(e => e.UptadtedAt)
                .HasMaxLength(6)
                .HasColumnName("uptadtedAt");
            entity.Property(e => e.UptadtedBy)
                .HasColumnType("int(11)")
                .HasColumnName("uptadtedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClienteCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Cliente__created__534D60F1");

            entity.HasOne(d => d.UptadtedByNavigation).WithMany(p => p.ClienteUptadtedByNavigations)
                .HasForeignKey(d => d.UptadtedBy)
                .HasConstraintName("FK__Cliente__uptadte__5441852A");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Nit).HasName("PRIMARY");

            entity
                .ToTable("contrato")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdCotizacion, "FK__Contrato__idCoti__66603565");

            entity.Property(e => e.Nit)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("nit");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdCotizacion)
                .HasColumnType("int(11)")
                .HasColumnName("idCotizacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RepresentanteLegal)
                .HasMaxLength(100)
                .HasColumnName("representanteLegal");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdCotizacion)
                .HasConstraintName("FK__Contrato__idCoti__66603565");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("PRIMARY");

            entity
                .ToTable("cotizacion")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.EditadoPor, "FK__Cotizacio__edita__619B8048");

            entity.HasIndex(e => e.IdCliente, "FK__Cotizacio__idCli__5EBF139D");

            entity.HasIndex(e => e.IdMotivo, "FK__Cotizacio__idMot__60A75C0F");

            entity.HasIndex(e => e.IdUsuario, "FK__Cotizacio__idUsu__5FB337D6");

            entity.Property(e => e.IdCotizacion)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idCotizacion");
            entity.Property(e => e.Comentarios).HasColumnName("comentarios");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.EditadoPor)
                .HasColumnType("int(11)")
                .HasColumnName("editadoPor");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)")
                .HasColumnName("estado");
            entity.Property(e => e.Expiracion)
                .HasMaxLength(6)
                .HasColumnName("expiracion");
            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.IdMotivo)
                .HasColumnType("int(11)")
                .HasColumnName("idMotivo");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(40)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");
            entity.Property(e => e.ValorIva)
                .HasPrecision(10, 2)
                .HasColumnName("valorIva");

            entity.HasOne(d => d.EditadoPorNavigation).WithMany(p => p.CotizacionEditadoPorNavigations)
                .HasForeignKey(d => d.EditadoPor)
                .HasConstraintName("FK__Cotizacio__edita__619B8048");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Cotizacio__idCli__5EBF139D");

            entity.HasOne(d => d.IdMotivoNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdMotivo)
                .HasConstraintName("FK__Cotizacio__idMot__60A75C0F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CotizacionIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Cotizacio__idUsu__5FB337D6");
        });

        modelBuilder.Entity<DetalleCotizacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacion).HasName("PRIMARY");

            entity
                .ToTable("detallecotizacion")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdCotizacion, "FK__DetalleCo__idCot__74AE54BC");

            entity.HasIndex(e => e.IdServicio, "FK__DetalleCo__idSer__75A278F5");

            entity.HasIndex(e => e.IdSucursal, "FK__DetalleCo__idSuc__76969D2E");

            entity.Property(e => e.IdDetalleCotizacion)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.CantidadServicios)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadServicios");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.DetalleServicio).HasColumnName("detalleServicio");
            entity.Property(e => e.IdCotizacion)
                .HasColumnType("int(11)")
                .HasColumnName("idCotizacion");
            entity.Property(e => e.IdServicio)
                .HasColumnType("int(11)")
                .HasColumnName("idServicio");
            entity.Property(e => e.IdSucursal)
                .HasColumnType("int(11)")
                .HasColumnName("idSucursal");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdCotizacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idCot__74AE54BC");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idSer__75A278F5");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idSuc__76969D2E");
        });

        modelBuilder.Entity<DetalleCotizacionInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("detallecotizacioninventario")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.CreatedBy, "FK__DetalleCo__creat__7B5B524B");

            entity.HasIndex(e => e.IdDetalleCotizacion, "FK__DetalleCo__idDet__7A672E12");

            entity.HasIndex(e => e.IdInventario, "FK__DetalleCo__idInv__7C4F7684");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CantidadAsignada)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadAsignada");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("createdBy");
            entity.Property(e => e.IdDetalleCotizacion)
                .HasColumnType("int(11)")
                .HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.IdInventario)
                .HasColumnType("int(11)")
                .HasColumnName("idInventario");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__DetalleCo__creat__7B5B524B");

            entity.HasOne(d => d.IdDetalleCotizacionNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.IdDetalleCotizacion)
                .HasConstraintName("FK__DetalleCo__idDet__7A672E12");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.IdInventario)
                .HasConstraintName("FK__DetalleCo__idInv__7C4F7684");
        });

        modelBuilder.Entity<DetalleCotizacionVariable>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacionVariables).HasName("PRIMARY");

            entity
                .ToTable("detallecotizacionvariables")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdDetalleCotizacion, "FK__DetalleCo__idDet__00200768");

            entity.HasIndex(e => e.IdVariablesEconomicas, "FK__DetalleCo__idVar__01142BA1");

            entity.Property(e => e.IdDetalleCotizacionVariables)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idDetalleCotizacionVariables");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdDetalleCotizacion)
                .HasColumnType("int(11)")
                .HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.IdVariablesEconomicas)
                .HasColumnType("int(11)")
                .HasColumnName("idVariablesEconomicas");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");
            entity.Property(e => e.Valor)
                .HasColumnType("bigint(20)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdDetalleCotizacionNavigation).WithMany(p => p.DetalleCotizacionVariables)
                .HasForeignKey(d => d.IdDetalleCotizacion)
                .HasConstraintName("FK__DetalleCo__idDet__00200768");

            entity.HasOne(d => d.IdVariablesEconomicasNavigation).WithMany(p => p.DetalleCotizacionVariables)
                .HasForeignKey(d => d.IdVariablesEconomicas)
                .HasConstraintName("FK__DetalleCo__idVar__01142BA1");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PRIMARY");

            entity
                .ToTable("inventario")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.CreatedBy, "FK__Inventari__creat__6D0D32F4");

            entity.HasIndex(e => e.IdCategoriaInventario, "FK__Inventari__idCat__6C190EBB");

            entity.Property(e => e.IdInventario)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idInventario");
            entity.Property(e => e.CantidadAsignada)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadAsignada");
            entity.Property(e => e.CantidadDisponible)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadDisponible");
            entity.Property(e => e.CantidadTotal)
                .HasColumnType("int(11)")
                .HasColumnName("cantidadTotal");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createAt");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("createdBy");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'0'")
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoriaInventario)
                .HasColumnType("int(11)")
                .HasColumnName("idCategoriaInventario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UptadedAt)
                .HasMaxLength(6)
                .HasColumnName("uptadedAt");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Inventari__creat__6D0D32F4");

            entity.HasOne(d => d.IdCategoriaInventarioNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCategoriaInventario)
                .HasConstraintName("FK__Inventari__idCat__6C190EBB");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PRIMARY");

            entity
                .ToTable("menu")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdMenu)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idMenu");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UptadtedAt)
                .HasMaxLength(6)
                .HasColumnName("uptadtedAt");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PRIMARY");

            entity
                .ToTable("menurol")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdMenu, "FK__MenuRol__idMenu__3D5E1FD2");

            entity.HasIndex(e => e.IdRol, "FK__MenuRol__idRol__3E52440B");

            entity.Property(e => e.IdMenuRol)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idMenuRol");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdMenu)
                .HasColumnType("int(11)")
                .HasColumnName("idMenu");
            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("idRol");
            entity.Property(e => e.UptadeAt)
                .HasMaxLength(6)
                .HasColumnName("uptadeAt");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__3D5E1FD2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__3E52440B");
        });

        modelBuilder.Entity<Motivo>(entity =>
        {
            entity.HasKey(e => e.IdMotivo).HasName("PRIMARY");

            entity
                .ToTable("motivo")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdMotivo)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idMotivo");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PRIMARY");

            entity
                .ToTable("numerodocumento")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdNumeroDocumento)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idNumeroDocumento");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.UltimoNumero)
                .HasColumnType("int(11)")
                .HasColumnName("ultimoNumero");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity
                .ToTable("rol")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idRol");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity
                .ToTable("servicio")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.TipoServicio, "FK__Servicio__tipoSe__4AB81AF0");

            entity.Property(e => e.IdServicio)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idServicio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoServicio)
                .HasColumnType("int(11)")
                .HasColumnName("tipoServicio");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.TipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.TipoServicio)
                .HasConstraintName("FK__Servicio__tipoSe__4AB81AF0");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PRIMARY");

            entity
                .ToTable("sucursal")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdCliente, "FK__Sucursal__idClie__59063A47");

            entity.Property(e => e.IdSucursal)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idSucursal");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Sucursal__idClie__59063A47");
        });

        //modelBuilder.Entity<Sysdiagram>(entity =>
        //{
        //    entity.HasKey(e => e.DiagramId).HasName("PRIMARY");

        //    entity
        //        .ToTable("sysdiagrams")
        //        .HasCharSet("utf8mb4")
        //        .UseCollation("utf8mb4_general_ci");

        //    entity.HasIndex(e => new { e.PrincipalId, e.Name }, "UK_principal_name").IsUnique();

        //    entity.Property(e => e.DiagramId)
        //        .ValueGeneratedNever()
        //        .HasColumnType("int(11)")
        //        .HasColumnName("diagram_id");
        //    entity.Property(e => e.Definition).HasColumnName("definition");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(160)
        //        .HasColumnName("name");
        //    entity.Property(e => e.PrincipalId)
        //        .HasColumnType("int(11)")
        //        .HasColumnName("principal_id");
        //    entity.Property(e => e.Version)
        //        .HasColumnType("int(11)")
        //        .HasColumnName("version");
        //});

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PRIMARY");

            entity
                .ToTable("tiposervicio")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.IdTipoServicio)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idTipoServicio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Formula)
                .HasMaxLength(250)
                .HasColumnName("formula");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdateAt)
                .HasMaxLength(6)
                .HasColumnName("updateAt");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity
                .ToTable("usuario")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdRol, "FK__Usuario__idRol__4222D4EF");

            entity.HasIndex(e => e.UsuarioRegistro, "FK__Usuario__usuario__4316F928");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .HasColumnName("contrasena");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("esActivo");
            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("idRol");
            entity.Property(e => e.LastConnection)
                .HasMaxLength(6)
                .HasColumnName("lastConnection");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.UptadedAt)
                .HasMaxLength(6)
                .HasColumnName("uptadedAt");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(200)
                .HasColumnName("urlFoto");
            entity.Property(e => e.UsuarioRegistro)
                .HasColumnType("int(11)")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__idRol__4222D4EF");

            entity.HasOne(d => d.UsuarioRegistroNavigation).WithMany(p => p.InverseUsuarioRegistroNavigation)
                .HasForeignKey(d => d.UsuarioRegistro)
                .HasConstraintName("FK__Usuario__usuario__4316F928");
        });

        modelBuilder.Entity<VariablesEconomicas>(entity =>
        {
            entity.HasKey(e => e.IdVariablesEconomicas).HasName("PRIMARY");

            entity
                .ToTable("variableseconomicas")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.CreatedBy, "FK__Variables__creat__4E88ABD4");

            entity.HasIndex(e => e.UpdatedBy, "FK__Variables__updat__4F7CD00D");

            entity.Property(e => e.IdVariablesEconomicas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idVariablesEconomicas");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("createdBy");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("int(11)")
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("uptatedAt");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VariablesEconomicaCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Variables__creat__4E88ABD4");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VariablesEconomicaUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__Variables__updat__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
