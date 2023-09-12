using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChTestPro.Models.DbTable
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? NombreProducto { get; set; }
        public int Ubicacion { get; set; }
        public string? PrecioDetal { get; set; }
        public string? PrecioMayor { get; set; }
        public int Existencias { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
