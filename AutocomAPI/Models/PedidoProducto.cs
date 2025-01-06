using System.Text.Json.Serialization;

public class PedidoProducto
{
    public int PedidoId { get; set; }
    [JsonIgnore]
    public Pedido? Pedido { get; set; }

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    public int Cantidad { get; set; }
}