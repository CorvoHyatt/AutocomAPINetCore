using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _context.Pedidos.Include(p => p.PedidoProductos).ToListAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPedido(int id)
    {
        var pedido = await _context.Pedidos.Include(p => p.PedidoProductos)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] Pedido pedido)
    {
        if (pedido == null) return BadRequest();

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePedido(int id, [FromBody] Pedido pedido)
    {
        if (id != pedido.Id) return BadRequest();

        _context.Entry(pedido).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        var pedido = await _context.Pedidos.Include(p => p.PedidoProductos)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null) return NotFound();

        if (pedido.PedidoProductos != null && pedido.PedidoProductos.Count > 0)
            return BadRequest("No se puede eliminar un pedido con productos asociados.");

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/productos")]
    public async Task<IActionResult> AddProductToPedido(int id, [FromBody] PedidoProducto pedidoProducto)
    {
        if (id != pedidoProducto.PedidoId) return BadRequest();

        _context.PedidoProductos.Add(pedidoProducto);
        await _context.SaveChangesAsync();
        return Ok(pedidoProducto);
    }

    [HttpGet("{id}/productos")]
    public async Task<IActionResult> GetProductosFromPedido(int id)
    {
        var productos = await _context.PedidoProductos
            .Where(pp => pp.PedidoId == id)
            .Include(pp => pp.Producto)
            .ToListAsync();
        return Ok(productos);
    }

}

