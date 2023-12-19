using _1670.Data;
using _1670.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _1670.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this._context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var product = await dbContext.Product.ToListAsync();
            return View(product);
        }


        public IActionResult New()
        {
            return View();
        }
    }
}