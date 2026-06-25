using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Velore_Studio.Models;

namespace Velore_Studio.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "Admin1234!")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index");
        }
        ViewBag.Error = "İstifadəçi adı və ya şifrə yanlışdır!";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        ViewBag.ProjectCount = await _context.Projects.CountAsync();
        ViewBag.TestimonialCount = await _context.Testimonials.CountAsync();
        ViewBag.UnreadMessageCount = await _context.ContactMessages.CountAsync(m => !m.IsRead);
        return View();
    }
    
    public async Task<IActionResult> Projects()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var projects = await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
        return View(projects);
    }
    
    public IActionResult CreateProject()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return RedirectToAction("Projects");
    }
    
    public async Task<IActionResult> DeleteProject(int id)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Projects");
    }
    
    public async Task<IActionResult> Testimonials()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var testimonials = await _context.Testimonials.ToListAsync();
        return View(testimonials);
    }

    public IActionResult CreateTestimonial()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        _context.Testimonials.Add(testimonial);
        await _context.SaveChangesAsync();
        return RedirectToAction("Testimonials");
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var testimonial = await _context.Testimonials.FindAsync(id);
        if (testimonial != null)
        {
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Testimonials");
    }
    
    public async Task<IActionResult> Messages()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var messages = await _context.ContactMessages.OrderByDescending(m => m.SentAt).ToListAsync();
        return View(messages);
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login");
        
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Messages");
    }
}