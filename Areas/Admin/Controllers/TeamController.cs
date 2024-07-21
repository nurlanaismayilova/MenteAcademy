using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitee.Models;
using WebApplication11.DAL;
using WebApplication11.Utilities;

namespace WebApplication11.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.Include(p => p.Profession).ToListAsync());
        }

        [HttpGet] // for visualization
        public async Task<IActionResult> Create()
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team? team)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            //if (!ModelState.IsValid) { return View(); }
            if (team == null) { return View(); }

            if (!team.ImageFile.CheckFileType("image")) { return View(); }
            if (team.ImageFile.CheckFileSize(2000)) { return View(); }

            string fileName = await team.ImageFile.SaveFileAsync(_environment.WebRootPath, "team");
            team.Image = fileName;

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");// nameof(Index)
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            Team? team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null) { NotFound(); return View(); }
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            if (team == null) { NotFound(); return View(); }
            Team? exists = await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
            if (exists == null) { NotFound(); return View(); }
            exists.Name = team.Name;
            exists.ProfessionId = team.ProfessionId;
            //exists.ImageFile = team.ImageFile;

            if (team.ImageFile != null)
            {
                if (!team.ImageFile.CheckFileType("image")) { return View(); }
                if (team.ImageFile.CheckFileSize(2000)) { return View(); }

                exists.ImageFile.DeleteFile(_environment.WebRootPath, "team", exists.Image);
                string fileName = await team.ImageFile.SaveFileAsync(_environment.WebRootPath, "team");
                exists.Image = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Team? team = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (team == null) { return View(); }
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}