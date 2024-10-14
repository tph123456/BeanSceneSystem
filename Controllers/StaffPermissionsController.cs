using BeanSceneSystem.Data;
using BeanSceneSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BeanSceneSystem.Controllers
{
    [Authorize]
    public class StaffPermissionsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StaffPermissionsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET StaffPermissions
        public async Task<IActionResult> Index()
        {
            var restaurantContext = dbContext.StaffPermissions.Include(s => s.Staff);
            return View(await restaurantContext.ToListAsync());
        }

        // GET StaffPermissions Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.StaffPermissions == null)
            {
                return NotFound();
            }

            var staffPermission = await dbContext.StaffPermissions
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.PermissionID == id);
            if (staffPermission == null)
            {
                return NotFound();
            }

            return View(staffPermission);
        }

        // GET StaffPermissions Create
        public IActionResult Create()
        {
            ViewData["StaffID"] = new SelectList(dbContext.Staffs, "StaffID", "Email");
            return View();
        }

        // POST StaffPermissions Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PermissionID,StaffID,TableName,PermissionType")] StaffPermission staffPermission)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(staffPermission);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffID"] = new SelectList(dbContext.Staffs, "StaffID", "Email", staffPermission.StaffID);
            return View(staffPermission);
        }

        // GET StaffPermissions Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.StaffPermissions == null)
            {
                return NotFound();
            }

            var staffPermission = await dbContext.StaffPermissions.FindAsync(id);
            if (staffPermission == null)
            {
                return NotFound();
            }
            ViewData["StaffID"] = new SelectList(dbContext.Staffs, "StaffID", "Email", staffPermission.StaffID);
            return View(staffPermission);
        }

        // POST StaffPermissions Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PermissionID,StaffID,TableName,PermissionType")] StaffPermission staffPermission)
        {
            if (id != staffPermission.PermissionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(staffPermission);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffPermissionExists(staffPermission.PermissionID))
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
            ViewData["StaffID"] = new SelectList(dbContext.Staffs, "StaffID", "Email", staffPermission.StaffID);
            return View(staffPermission);
        }

        // GET StaffPermissions Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.StaffPermissions == null)
            {
                return NotFound();
            }

            var staffPermission = await dbContext.StaffPermissions
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.PermissionID == id);
            if (staffPermission == null)
            {
                return NotFound();
            }

            return View(staffPermission);
        }

        // POST StaffPermissions Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.StaffPermissions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StaffPermissions'  is null.");
            }
            var staffPermission = await dbContext.StaffPermissions.FindAsync(id);
            if (staffPermission != null)
            {
                dbContext.StaffPermissions.Remove(staffPermission);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffPermissionExists(int id)
        {
            return (dbContext.StaffPermissions?.Any(e => e.PermissionID == id)).GetValueOrDefault();
        }
    }
}
