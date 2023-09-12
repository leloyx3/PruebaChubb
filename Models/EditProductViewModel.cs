using ChTestPro.Models.DbTable;

namespace ChTestPro.Models
{
    public class EditProductViewModel
    {
        public Producto Producto { get; set; }
        public List<Ubicacion> Ubicaciones { get; set; }
    }
}
