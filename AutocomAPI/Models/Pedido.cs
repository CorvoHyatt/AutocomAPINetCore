using System.ComponentModel.DataAnnotations;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public int ClienteId { get; set; }

    public ICollection<PedidoProducto>? PedidoProductos { get; set; }
}
