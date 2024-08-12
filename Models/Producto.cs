// Producto.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductoService.Models
{
    [Table("producto")]
    public class Producto
    {
        [Key]
        [Column("productoid")]
        public int ProductoId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } 

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("precio")]
        public decimal Precio { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [ForeignKey("Categoria")]
        [Column("categoriaid")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [Column("rutaimagen")]
        public string RutaImagen { get; set; }

        [Column("fechacreacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column("usuarioid")]
        public int? UsuarioId { get; set; }
    }
}
