using BeanSceneSystem.Data;
using BeanSceneSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BeanSceneSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ReservationsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET Reservations
        public async Task<IActionResult> Index()
        {
            var restaurantContext = dbContext.Reservations.Include(r => r.Table).Include(r => r.Schedule);
            return View(await restaurantContext.ToListAsync());
        }

        // GET Reservations Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await dbContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Schedule)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET Reservations Create
        public IActionResult Create()
        {
            ViewData["TableID"] = new SelectList(dbContext.Tables, "TableID", "TableName");
            ViewData["SittingID"] = new SelectList(dbContext.Schedules, "SittingID", "SType");
            return View();
        }

        // POST Reservations Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,SittingID,GuestName,Email,Phone,StartTime,Duration,NumOfGuests,ReservationSource,Notes,Status,TableID,MemberID")] Reservation reservation)
        {
            ViewData["TableID"] = new SelectList(dbContext.Tables, "TableID", "TableName", reservation.TableID);
            ViewData["SittingID"] = new SelectList(dbContext.Schedules, "SittingID", "SType", reservation.SittingID);

            // Check if the table already has a "Seated" or "Confirmed" reservation
            var existingReservation = await dbContext.Reservations
                .Where(r => r.TableID == reservation.TableID && (r.Status == "Seated" || r.Status == "Confirmed"))
                .FirstOrDefaultAsync();

            if (existingReservation != null)
            {
                ModelState.AddModelError("", "The selected table is already reserved, please select a different table.");
            }

            if (ModelState.IsValid)
            {
                dbContext.Add(reservation);
                await dbContext.SaveChangesAsync();
                await UpdateTableStatus(reservation.TableID, reservation.Status);
                return RedirectToAction(nameof(Index));
            }

            return View(reservation);
        }

        // GET Reservations Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["TableID"] = new SelectList(dbContext.Tables, "TableID", "TableName", reservation.TableID);
            ViewData["SittingID"] = new SelectList(dbContext.Schedules, "SittingID", "SType", reservation.SittingID);
            return View(reservation);
        }

        // POST Reservations Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,SittingID,GuestName,Email,Phone,StartTime,Duration,NumOfGuests,ReservationSource,Notes,Status,TableID,MemberID")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(reservation);
                    await dbContext.SaveChangesAsync();
                    await UpdateTableStatus(reservation.TableID, reservation.Status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationID))
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
            ViewData["TableID"] = new SelectList(dbContext.Tables, "TableID", "TableName", reservation.TableID);
            ViewData["SittingID"] = new SelectList(dbContext.Schedules, "SittingID", "SType", reservation.SittingID);
            return View(reservation);
        }

        // GET Reservations Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await dbContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Schedule)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST Reservations Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations' is null.");
            }
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
                await dbContext.SaveChangesAsync();
                await UpdateTableStatus(reservation.TableID, "Free");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return (dbContext.Reservations?.Any(e => e.ReservationID == id)).GetValueOrDefault();
        }

        private async Task UpdateTableStatus(int? tableID, string reservationStatus)
        {
            if (tableID.HasValue)
            {
                var table = await dbContext.Tables.FindAsync(tableID.Value);
                if (table != null)
                {
                    if (reservationStatus == "Seated" || reservationStatus == "Confirmed")
                    {
                        table.TableStatus = "Booked";
                    }
                    else
                    {
                        table.TableStatus = "Free";
                    }
                    dbContext.Update(table);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
