using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitee.Models;
using WebApplication11.DAL;

namespace WebApplication11.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfessionController : Controller
    {
        private readonly AppDbContext _context;

        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Professions.ToListAsync());
        }

        [HttpGet] // for visualization
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profession profession)
        {
            if (!ModelState.IsValid) { return View(); }
            if (profession == null) { return View(); }
            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");// nameof(Index)
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Profession? profession = await _context.Professions.FirstOrDefaultAsync(p => p.Id == id);
            if (profession == null) { return View(); }
            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Profession profession)
        {
            Profession? exists = await _context.Professions.FirstOrDefaultAsync(p => p.Id == profession.Id);
            if (profession == null) { return View(); }
            if (exists == null) { return View(); }
            exists.Name = profession.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Profession? profession = await _context.Professions.FirstOrDefaultAsync(p => p.Id == id);
            if (profession == null) { return View(); }
            _context.Remove(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}