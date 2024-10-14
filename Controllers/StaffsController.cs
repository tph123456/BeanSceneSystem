using BeanSceneSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeanSceneSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeanSceneSystem.Controllers
{
    [Authorize]
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StaffsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET Staffs
        public async Task<IActionResult> Index()
        {
            return dbContext.Staffs != null ?
                        View(await dbContext.Staffs.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Staffs'  is null.");
        }

        // GET Staffs Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Staffs == null)
            {
                return NotFound();
            }

            var staff = await dbContext.Staffs
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET Staffs Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Staffs Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffID,StaffType,FirstName,LastName,Email,Phone,Password")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(staff);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET Staffs Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Staffs == null)
            {
                return NotFound();
            }

            var staff = await dbContext.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST Staffs Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,StaffType,FirstName,LastName,Email,Phone,Password")] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(staff);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
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
            return View(staff);
        }

        // GET Staffs Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Staffs == null)
            {
                return NotFound();
            }

            var staff = await dbContext.Staffs
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST Staffs Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Staffs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Staffs'  is null.");
            }
            var staff = await dbContext.Staffs.FindAsync(id);
            if (staff != null)
            {
                dbContext.Staffs.Remove(staff);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return (dbContext.Staffs?.Any(e => e.StaffID == id)).GetValueOrDefault();
        }
    }
}
