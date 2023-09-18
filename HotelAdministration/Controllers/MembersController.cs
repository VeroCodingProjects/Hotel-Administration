using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdministration.Areas.Identity.Data;
using HotelAdministration.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelAdministration.Controllers
{
    public class MembersController : Controller
    {
        private readonly TeamMembersDb _context;

        public MembersController(TeamMembersDb context)
        {
            _context = context;
        }

        // GET: Members
        [Authorize(Roles = "Admin, Employees")]
        public async Task<IActionResult> Index()
        {
              return _context.Members != null ? 
                          View(await _context.Members.ToListAsync()) :
                          Problem("Entity set 'TeamMembersDb.Members'  is null.");
        }

        // GET: Members/Details/5
        [Authorize(Roles = "Admin, Employees")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var teamMembers = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMembers == null)
            {
                return NotFound();
            }

            return View(teamMembers);
        }

        // GET: Members/Create
        [Authorize(Roles = "Admin, Employees")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone,Role")] TeamMembers teamMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamMembers);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var teamMembers = await _context.Members.FindAsync(id);
            if (teamMembers == null)
            {
                return NotFound();
            }
            return View(teamMembers);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,Role")] TeamMembers teamMembers)
        {
            if (id != teamMembers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMembersExists(teamMembers.Id))
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
            return View(teamMembers);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var teamMembers = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMembers == null)
            {
                return NotFound();
            }

            return View(teamMembers);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'TeamMembersDb.Members'  is null.");
            }
            var teamMembers = await _context.Members.FindAsync(id);
            if (teamMembers != null)
            {
                _context.Members.Remove(teamMembers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMembersExists(int id)
        {
          return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
