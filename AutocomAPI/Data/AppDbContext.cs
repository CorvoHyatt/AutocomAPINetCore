using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<PedidoProducto> PedidoProductos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PedidoProducto>()
            .HasKey(pp => new { pp.PedidoId, pp.ProductoId });

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Pedido)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.PedidoId);

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Producto)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.ProductoId);
    }
}
