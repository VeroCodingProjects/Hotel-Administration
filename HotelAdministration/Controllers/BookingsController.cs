using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdministration.Data;
using HotelAdministration.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelAdministration.Controllers
{
    public class BookingsController : Controller
    {
        private readonly HotelAdministrationContext _context;

        public BookingsController(HotelAdministrationContext context)
        {
            _context = context;
        }

        // GET: Bookings
        [Authorize(Roles = "Admin, Employees")]
        public async Task<IActionResult> Index()
        {
              return _context.Bookings != null ? 
                          View(await _context.Bookings.ToListAsync()) :
                          Problem("Entity set 'HotelAdministrationContext.Bookings'  is null.");
        }

        // GET: Bookings/Details/5
        [Authorize(Roles = "Admin, Employees")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        [Authorize(Roles = "Admin, Employees")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameClient,NameSeller,StartDate,EndDate,NumberRoom")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                // Check if the room is already booked during the requested dates
                    var existingBookings = await _context.Bookings
                    .Where(b => b.NumberRoom == bookings.NumberRoom)
                    .Where(b => b.StartDate <= bookings.EndDate && b.EndDate >= bookings.StartDate)
                    .ToListAsync();

                if (existingBookings.Count > 0)
                {
                    ModelState.AddModelError("StartDate", "This room is already booked for the requested dates.");
                    return View(bookings);
                }
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameClient,NameSeller,StartDate,EndDate,NumberRoom")] Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.Id))
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
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'HotelAdministrationContext.Bookings'  is null.");
            }
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
          return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GetRoomBookings()
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }

            var roomBookings = await _context.Bookings.ToListAsync();

            // Format the data as FullCalendar events
            var formattedData = roomBookings.Select(booking => new
            {
                id = booking.Id,
                title = $"Room {booking.NumberRoom} - {booking.NameClient}",
                start = booking.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = booking.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            });

            return Json(formattedData);
        }
    }
}
