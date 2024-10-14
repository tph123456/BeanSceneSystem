using BeanSceneSystem.Data;
using BeanSceneSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BeanSceneSystem.Controllers
{
    public class AreasController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AreasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET Areas
        public async Task<IActionResult> Index()
        {
            return dbContext.Areas != null ?
                        View(await dbContext.Areas.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Areas'  is null.");
        }

        // GET Areas Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Areas == null)
            {
                return NotFound();
            }

            var area = await dbContext.Areas
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET Create Areas
        public IActionResult Create()
        {
            return View();
        }


        // POST Create Areas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AreaID,AreaName")] Area area)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(area);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET Areas Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Areas == null)
            {
                return NotFound();
            }

            var area = await dbContext.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST Areas Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AreaID,AreaName")] Area area)
        {
            if (id != area.AreaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(area);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.AreaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET Areas Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Areas == null)
            {
                return NotFound();
            }

            var area = await dbContext.Areas
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST Areas Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Areas == null)
            {
                return Problem("Entity set 'RestaurantContext.Areas'  is null.");
            }
            var area = await dbContext.Areas.FindAsync(id);
            if (area != null)
            {
                dbContext.Areas.Remove(area);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool AreaExists(int id)
        {
            return (dbContext.Areas?.Any(e => e.AreaID == id)).GetValueOrDefault();
        }
    }
}
