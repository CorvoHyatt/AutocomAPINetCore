using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductos()
    {
        var productos = await _context.Productos.ToListAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        return Ok(producto);
    }
    [HttpPost]
    public async Task<IActionResult> CreateProducto([FromBody] Producto producto)
    {
        if (producto == null) return BadRequest();
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
    {
        if (id != producto.Id) return BadRequest();
        _context.Entry(producto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> AddProductoToPedido(int pedidoId, int productoId)
    {
        var pedido = await _context.Pedidos.Include(p => p.PedidoProductos)
            .FirstOrDefaultAsync(p => p.Id == pedidoId);
        if (pedido == null) return NotFound();
        var producto = await _context.Productos.FindAsync(productoId);
        if (producto == null) return NotFound();
        var pedidoProducto = new PedidoProducto { Pedido = pedido, Producto = producto };
        _context.PedidoProductos.Add(pedidoProducto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> RemoveProductoFromPedido(int pedidoId, int productoId)
    {
        var pedidoProducto = await _context.PedidoProductos
            .FirstOrDefaultAsync(pp => pp.PedidoId == pedidoId && pp.ProductoId == productoId);
        if (pedidoProducto == null) return NotFound();
        _context.PedidoProductos.Remove(pedidoProducto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}
