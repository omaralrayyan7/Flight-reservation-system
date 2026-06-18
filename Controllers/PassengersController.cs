using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;

namespace FlightReservationApp_f.Controllers
{
    public class PassengersController : Controller
    {
        private readonly DataContext _context;

        public PassengersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
            => View(await _context.Passenger.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var passenger = await _context.Passenger.FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null) return NotFound();
            return View(passenger);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerId,FullName,Email,ContactNumber")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var passenger = await _context.Passenger.FindAsync(id);
            if (passenger == null) return NotFound();
            return View(passenger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassengerId,FullName,Email,ContactNumber")] Passenger passenger)
        {
            if (id != passenger.PassengerId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.PassengerId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var passenger = await _context.Passenger.FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null) return NotFound();
            return View(passenger);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passenger.FindAsync(id);
            if (passenger != null) _context.Passenger.Remove(passenger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(int id) => _context.Passenger.Any(e => e.PassengerId == id);
    }
}
