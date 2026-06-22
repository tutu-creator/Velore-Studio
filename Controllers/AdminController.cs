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

    public async Task<IActionResult> Index()

    {
        ViewBag.ProjectCount = await _context.Projects.CountAsync();
        ViewBag.TestimonialCount = await _context.Testimonials.CountAsync();
        ViewBag.UnreadMessageCount = await _context.ContactMessages.CountAsync(m => !m.IsRead);
        return View();
    }
    
    public async Task<IActionResult> Projects()
    {
        var projects= await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
        return View(projects);
    }
    
    public IActionResult CreateProject()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project)
    {
        if (ModelState.IsValid)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Projects");
        }
        return View(project);
    }
    
    public async Task<IActionResult> DeleteProject(int id)
    {
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
        var testimonials = await _context.Testimonials.ToListAsync();
        return View(testimonials);
    }

    public IActionResult CreateTestimonial()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
    {
        if (ModelState.IsValid)
        {
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Testimonials");
        }
        return View(testimonial);
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
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
        var messages = await _context.ContactMessages.OrderByDescending(m => m.SentAt).ToListAsync();
        return View(messages);
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Messages");
    }
}



