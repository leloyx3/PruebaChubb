using ChTestPro.Data;
using ChTestPro.Models;
using ChTestPro.Models.DbTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChTestPro.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ChTestDbContext _chTestDbContext;

        public ProductoController(ILogger<ProductoController> logger, IConfiguration configuration, ChTestDbContext chTestDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _chTestDbContext = chTestDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditarProducto(int Id)
        {
            try
            {
                EditProductViewModel editProductViewModel = new EditProductViewModel();

                var dataProductModel = await _chTestDbContext.Productos
                    .Where(x => x.Id == Id)
                    .FirstOrDefaultAsync();

                var dataUbicacionModel = await _chTestDbContext.Ubicacion
                    .ToListAsync();

                editProductViewModel.Producto = dataProductModel;
                editProductViewModel.Ubicaciones = dataUbicacionModel;

                return View(editProductViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOut", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditarProducto(int Id, string NombreProducto, int Ubicacion, string PrecioDetal, string PrecioMayor, int Existencias, string Estado)
        {
            try
            {
                var dataProduct = await _chTestDbContext.Productos
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

                var dataUbicacionModel = await _chTestDbContext.Ubicacion
                    .AsNoTracking()
                    .Where(x => x.Id == Ubicacion)
                    .FirstOrDefaultAsync();

                if (dataUbicacionModel != null && Existencias > dataUbicacionModel.CapacidadMaxima)
                {
                    EditProductViewModel editProductViewModel = new EditProductViewModel();

                    var dataProductModel = await _chTestDbContext.Productos
                        .Where(x => x.Id == Id)
                        .FirstOrDefaultAsync();

                    var dataUbicacionModelTwo = await _chTestDbContext.Ubicacion
                        .ToListAsync();

                    editProductViewModel.Producto = dataProductModel;
                    editProductViewModel.Ubicaciones = dataUbicacionModelTwo;

                    ModelState.AddModelError("", string.Format("Las existencias del producto no pueden ser mayor a la capacidad máxima de la estiba, la cual es de {0} de almacenacimiento", dataUbicacionModel.CapacidadMaxima));
                    return View(editProductViewModel);
                }

                if (dataProduct != null)
                {
                    dataProduct.NombreProducto = NombreProducto;
                    dataProduct.Ubicacion = Ubicacion;
                    dataProduct.PrecioDetal = PrecioDetal;
                    dataProduct.PrecioMayor = PrecioMayor;
                    dataProduct.Existencias = Existencias;
                    dataProduct.Estado = (Estado == "1");
                    dataProduct.FechaModificacion = DateTime.Now;
                    _chTestDbContext.Productos.Update(dataProduct);
                    await _chTestDbContext.SaveChangesAsync();
                }
                else
                {
                    EditProductViewModel editProductViewModel = new EditProductViewModel();

                    var dataProductModel = await _chTestDbContext.Productos
                        .Where(x => x.Id == Id)
                        .FirstOrDefaultAsync();

                    var dataUbicacionModelTwo = await _chTestDbContext.Ubicacion
                        .ToListAsync();

                    editProductViewModel.Producto = dataProductModel;
                    editProductViewModel.Ubicaciones = dataUbicacionModelTwo;
                    ModelState.AddModelError("", "Producto no existe en base de datos.");
                    return View(editProductViewModel);
                }

                return RedirectToAction("Index", "Menu");
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOut", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarProducto()
        {
            try
            {
                var dataUbicacionModel = await _chTestDbContext.Ubicacion
                .ToListAsync();

                return View(dataUbicacionModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOut", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarProducto(string NombreProducto, int Ubicacion, string PrecioDetal, string PrecioMayor, int Existencias, string Estado)
        {
            try
            {
                var dataUbicacionModel = await _chTestDbContext.Ubicacion
                .AsNoTracking()
                .Where(x => x.Id == Ubicacion)
                .FirstOrDefaultAsync();

                if (dataUbicacionModel != null && Existencias > dataUbicacionModel.CapacidadMaxima)
                {
                    var dataUbicacionModelTwo = await _chTestDbContext.Ubicacion
                    .ToListAsync();
                    ModelState.AddModelError("", string.Format("Las existencias del producto no pueden ser mayor a la capacidad máxima de la estiba, la cual es de {0} de almacenacimiento", dataUbicacionModel.CapacidadMaxima));
                    return View(dataUbicacionModelTwo);
                }

                Producto dataProductModel = new Producto()
                {
                    NombreProducto = NombreProducto,
                    Ubicacion = Ubicacion,
                    PrecioDetal = PrecioDetal,
                    PrecioMayor = PrecioMayor,
                    Existencias = Existencias,
                    Estado = (Estado == "1"),
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _chTestDbContext.Productos.Add(dataProductModel);
                await _chTestDbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Menu");
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOut", "Home");
            }
        }
    }
}
