// Categoria.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductoService.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        [Column("categoriaid")]
        public int CategoriaId { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
