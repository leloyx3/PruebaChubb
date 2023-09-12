using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChTestPro.Models.DbTable
{
    [Table("Ubicacion")]
    public class Ubicacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Descripcion { get; set; }
        public int NumeroPosiciones { get; set; }
        public int CapacidadMaxima { get; set; }
        public Producto Productos { get; set; }
    }
}
