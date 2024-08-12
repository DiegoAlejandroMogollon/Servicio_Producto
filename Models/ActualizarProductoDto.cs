
namespace ProductoService.Models
{
 
public class ActualizarProductoDto
{
    public string Nombre { get; set; } =string.Empty;
    public string Descripcion { get; set; } =string.Empty;
    public decimal? Precio { get; set; }
    public int? Stock { get; set; }
    public int? CategoriaId { get; set; }
    public IFormFile? File { get; set; }

    public string RutaImagen { get; set; } =string.Empty;
}
}