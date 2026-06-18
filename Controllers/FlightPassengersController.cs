using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightReservationApp_f.Controllers
{
    public class FlightPassengersController : Controller
    {
        private readonly DataContext _context;

        public FlightPassengersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fp = _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger);
            return View(await fp.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var fp = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fp == null) return NotFound();
            return View(fp);
        }

        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber");
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightId,PassengerId")] FlightPassenger flightPassenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightPassenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", flightPassenger.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", flightPassenger.PassengerId);
            return View(flightPassenger);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var fp = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fp == null) return NotFound();
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", fp.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", fp.PassengerId);
            return View(fp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightId,PassengerId")] FlightPassenger flightPassenger)
        {
            if (id != flightPassenger.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightPassenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightPassengerExists(flightPassenger.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", flightPassenger.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", flightPassenger.PassengerId);
            return View(flightPassenger);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var fp = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fp == null) return NotFound();
            return View(fp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fp = await _context.FlightPassenger.FindAsync(id);
            if (fp != null) _context.FlightPassenger.Remove(fp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightPassengerExists(int id) => _context.FlightPassenger.Any(e => e.Id == id);
    }
}
