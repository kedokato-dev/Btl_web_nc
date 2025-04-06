using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Btl_web_nc.Models;
using Btl_web_nc.Data;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Controllers;

public class HomeController : Controller
{
            private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy danh sách các newsletters có sẵn
            var newsletters = await _context.Newsletters
                .Include(n => n.Topic)
                .ToListAsync();
                
            return View(newsletters);
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
