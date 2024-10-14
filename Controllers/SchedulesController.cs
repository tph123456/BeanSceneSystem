using BeanSceneSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeanSceneSystem.Models;

namespace BeanSceneSystem.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SchedulesController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET Schedules
        public async Task<IActionResult> Index()
        {
            return dbContext.Schedules != null ?
                        View(await dbContext.Schedules.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Schedules'  is null.");
        }

        // GET Schedules Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await dbContext.Schedules
                .FirstOrDefaultAsync(m => m.SittingID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET SchedulesCreate
        public IActionResult Create()
        {
            return View();
        }

        // POST Schedules Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SittingID,SType,StartDateTime,EndDateTime,SCapacity,Status")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(schedule);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET Schedules Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST Schedules Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SittingID,SType,StartDateTime,EndDateTime,SCapacity,Status")] Schedule schedule)
        {
            if (id != schedule.SittingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(schedule);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!scheduleExists(schedule.SittingID))
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
            return View(schedule);
        }

        // GET Schedules Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await dbContext.Schedules
                .FirstOrDefaultAsync(m => m.SittingID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST Schedules Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Schedules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schedules'  is null.");
            }
            var schedule = await dbContext.Schedules.FindAsync(id);
            if (schedule != null)
            {
                dbContext.Schedules.Remove(schedule);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool scheduleExists(int id)
        {
            return (dbContext.Schedules?.Any(e => e.SittingID == id)).GetValueOrDefault();
        }
    }
}
