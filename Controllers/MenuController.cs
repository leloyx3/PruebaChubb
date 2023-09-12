using ChTestPro.Data;
using ChTestPro.Models;
using ChTestPro.Models.DbTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Web;

namespace ChTestPro.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ChTestDbContext _chTestDbContext;

        public MenuController(ILogger<MenuController> logger, IConfiguration configuration, ChTestDbContext chTestDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _chTestDbContext = chTestDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                DataTable dataTable = GetDataTableWithData();

                var model = new DataTableViewModel
                {
                    DataTable = dataTable
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOut", "Home");
            }
        }

        private DataTable GetDataTableWithData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                var dataProduct = _chTestDbContext.Productos
                .AsNoTracking()
                .Include(z => z.Ubicaciones)
                .Where(x => x.Estado)
                .ToList()
                .OrderBy(x => x.Id);

                dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("NombreProducto", typeof(string));
                dataTable.Columns.Add("Ubicacion", typeof(string));
                dataTable.Columns.Add("PrecioDetal", typeof(string));
                dataTable.Columns.Add("PrecioMayor", typeof(string));
                dataTable.Columns.Add("Existencias", typeof(string));
                dataTable.Columns.Add("Acciones");

                foreach (var item in dataProduct)
                {
                    dataTable.Rows.Add(
                        item.Id,
                        item.NombreProducto,
                        item.Ubicaciones.Descripcion,
                        item.PrecioDetal,
                        item.PrecioMayor,
                        item.Existencias
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dataTable;
        }
    }
}
