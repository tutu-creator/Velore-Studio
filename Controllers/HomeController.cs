using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Velore_Studio.Models;

namespace Velore_Studio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }   

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Portfolio()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SendMessage(ContactMessage message)
    {
     
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Contact");
        }
        return View("Contact");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}