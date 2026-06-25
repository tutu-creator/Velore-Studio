using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Velore_Studio.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IActionResult> Index()
    {
        var projects = await _context.Projects
            .Where(p => p.IsFeatured)
            .Take(2)
            .ToListAsync();
    
        var testimonials = await _context.Testimonials.ToListAsync();
    
        ViewBag.Projects = projects;
        ViewBag.Testimonials = testimonials;
    
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> Portfolio()
    {
        var projects = await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
        ViewBag.Projects = projects;
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SendMessage(ContactMessage formMessage)
    {
        _context.ContactMessages.Add(formMessage);
        await _context.SaveChangesAsync();
        return RedirectToAction("Contact");
     
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}