using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitee.Models;
using WebApplication11.DAL;

namespace WebApplication11.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        [HttpGet] // for visualization
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            //if (!ModelState.IsValid) { return View(); }
            if (service == null) { return View(); }
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");// nameof(Index)
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (service == null) { return View(); }
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service service)
        {
            Service? exists = await _context.Services.FirstOrDefaultAsync(p => p.Id == service.Id);
            if (service == null) { return View(); }
            if (exists == null) { return View(); }
            exists.Name = service.Name;
            exists.Description = service.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (service == null) { return View(); }
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}