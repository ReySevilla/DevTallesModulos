using DevTalles.Data;
using DevTalles.Models;
using DevTalles.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DevTalles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            HomeVM model = new()
            {
                Cursos = db.Cursos.Include(c => c.Categoria).Include(sb => sb.SubCategoria),
                Categorias = db.Categorias.ToList(),
                CursosDisponibles = db.Cursos.Any()
            };
            return View(model);
        }

		public  IActionResult Detalle(int id)
		{
            DetalleProductoVM model = new()
            {
                curso =  db.Cursos.Include(c => c.Categoria).Include(sb => sb.SubCategoria).Where(c => c.Id == id).FirstOrDefault(),
                ExisteEnCarro = false
				
			};
			return View(model);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}