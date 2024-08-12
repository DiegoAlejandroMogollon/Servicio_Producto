            using Microsoft.AspNetCore.Mvc;
            using Microsoft.EntityFrameworkCore;
            using ProductoService.Models;
            using System.Collections.Generic;
            using System.IO;
            using System.Linq;
            using System.Threading.Tasks;

            namespace ProductoService.Controllers
            {
                [Route("api/producto")]
                [ApiController]
                public class GestionProductoController : ControllerBase
                {
                    private readonly ApplicationDbContext _context;

                    public GestionProductoController(ApplicationDbContext context)
                    {
                        _context = context;
                    }

                [HttpPost("agregarProducto")]
            public async Task<IActionResult> CreateProducto([FromForm] string nombre, [FromForm] string descripcion, [FromForm] decimal precio, [FromForm] int stock, [FromForm] int categoriaId,[FromForm] int UsuarioId, IFormFile file)
            {
                var producto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaId = categoriaId,
                    UsuarioId = UsuarioId,
                    FechaCreacion = DateTime.UtcNow  
                };

                if (file != null && file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/images", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    producto.RutaImagen = $"/images/{file.FileName}";
                }

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProductoById), new { id = producto.ProductoId }, producto);
            }

            [HttpGet("obtenerProducto/{id}")]
                    public async Task<ActionResult<Producto>> GetProductoById(int id)
                    {
                        var producto = await _context.Productos
                            .Include(p => p.Categoria)
                            .FirstOrDefaultAsync(p => p.ProductoId == id);

                        if (producto == null)
                        {
                            return NotFound();
                        }

                        return Ok(producto);
                    }

                    [HttpGet("obtenerProductos")]
                    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
                    {
                        var productos = await _context.Productos
                            .Include(p => p.Categoria) 
                            .ToListAsync();

                        return Ok(productos);
                    }

[HttpPut("modificarProducto/{id}")]
public async Task<IActionResult> PutProducto(int id, ActualizarProductoDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var existingProducto = await _context.Productos.FindAsync(id);

    if (existingProducto == null)
    {
        return NotFound();
    }

    if (!string.IsNullOrEmpty(dto.Nombre))
    {
        existingProducto.Nombre = dto.Nombre;
    }

    if (!string.IsNullOrEmpty(dto.Descripcion))
    {
        existingProducto.Descripcion = dto.Descripcion;
    }

    if (dto.Precio.HasValue)
    {
        existingProducto.Precio = dto.Precio.Value;
    }

    if (dto.Stock.HasValue)
    {
        existingProducto.Stock = dto.Stock.Value;
    }

    if (dto.CategoriaId.HasValue)
    {
        existingProducto.CategoriaId = dto.CategoriaId.Value;
    }

    if (dto.File != null && dto.File.Length > 0)
    {
        var newFileName = Path.GetFileName(dto.File.FileName);
        var filePath = Path.Combine("wwwroot/images", newFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }
        existingProducto.RutaImagen = $"/images/{newFileName}";
    }

    existingProducto.FechaCreacion = existingProducto.FechaCreacion.ToUniversalTime();

    _context.Entry(existingProducto).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}


                    [HttpDelete("eliminarProducto/{id}")]
                    public async Task<IActionResult> DeleteProducto(int id)
                    {
                        var producto = await _context.Productos.FindAsync(id);
                        if (producto == null)
                        {
                            return NotFound();
                        }

                        _context.Productos.Remove(producto);
                        await _context.SaveChangesAsync();

                        return NoContent();
                    }
                }
            }
