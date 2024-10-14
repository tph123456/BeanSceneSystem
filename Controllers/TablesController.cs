using BeanSceneSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanSceneSystem.Models;

namespace BeanSceneSystem.Controllers
{
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TablesController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET Tables
        public async Task<IActionResult> Index()
        {
            var context = dbContext.Tables.Include(r => r.Area);
            return View(await context.ToListAsync());
        }

        // GET Tables Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Tables == null)
            {
                return NotFound();
            }

            var table = await dbContext.Tables
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.TableID == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET Tables Create
        public IActionResult Create()
        {
            ViewData["AreaID"] = new SelectList(dbContext.Areas, "AreaID", "AreaName");
            return View();
        }

        // POST Tables Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TableID, AreaID, TableName, TableStatus")] Table table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Add(table);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception or handle it appropriately
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                    // You can log the exception for detailed debugging
                    Console.WriteLine(ex);
                }
            }

            // If ModelState is not valid, populate ViewData and return the view with errors
            ViewData["AreaID"] = new SelectList(dbContext.Areas, "AreaID", "AreaName", table.AreaID);
            return View(table);
        }



        // GET Tables Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Tables == null)
            {
                return NotFound();
            }

            var table = await dbContext.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            ViewData["AreaID"] = new SelectList(dbContext.Areas, "AreaID", "AreaName", table.AreaID);
            return View(table);
        }

        // POST Tables Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TableID,AreaID,TableName,TableStatus")] Table table)
        {
            if (id != table.TableID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(table);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.TableID))
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
            ViewData["AreaID"] = new SelectList(dbContext.Areas, "AreaID", "AreaName", table.AreaID);
            return View(table);
        }

        // GET Tables Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Tables == null)
            {
                return NotFound();
            }

            var table = await dbContext.Tables
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.TableID == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // POST Tables Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Tables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tables'  is null.");
            }
            var table = await dbContext.Tables.FindAsync(id);
            if (table != null)
            {
                dbContext.Tables.Remove(table);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return (dbContext.Tables?.Any(e => e.TableID == id)).GetValueOrDefault();
        }
    }
}
