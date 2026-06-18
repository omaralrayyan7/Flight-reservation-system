using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: FlightPassengers
        public async Task<IActionResult> Index()
        {
            // Fetching flight-passenger associations with related data (Flight and Passenger)
            var flightPassengers = _context.FlightPassenger
                                           .Include(f => f.Flight)  // Include Flight details
                                           .Include(p => p.Passenger)  // Include Passenger details
                                           .ToListAsync();
            return View(await flightPassengers);  // Return to the view with the flight-passenger data
        }

        // GET: FlightPassengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            // Fetching the FlightPassenger and its related data (Flight and Passenger)
            var flightPassenger = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flightPassenger == null)
            {
                return NotFound(); // If the FlightPassenger is not found, return NotFound
            }

            return View(flightPassenger); // Return the FlightPassenger details to the view
        }

        // GET: FlightPassengers/Create
        public IActionResult Create()
        {
            // Populating the Flight and Passenger dropdowns
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber");
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email");
            return View(); // Return the Create view
        }

        // POST: FlightPassengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightId,PassengerId")] FlightPassenger flightPassenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightPassenger); // Add the new flight-passenger relationship
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index (list) page
            }

            // If the model state is invalid, repopulate the dropdowns and return to the Create view
            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", flightPassenger.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", flightPassenger.PassengerId);
            return View(flightPassenger); // Return the flight-passenger entity to the view
        }

        // GET: FlightPassengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            var flightPassenger = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flightPassenger == null)
            {
                return NotFound(); // If the FlightPassenger is not found, return NotFound
            }

            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", flightPassenger.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", flightPassenger.PassengerId);
            return View(flightPassenger); // Return the FlightPassenger to the Edit view
        }

        // POST: FlightPassengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightId,PassengerId")] FlightPassenger flightPassenger)
        {
            if (id != flightPassenger.Id)
            {
                return NotFound(); // If the ids do not match, return NotFound
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightPassenger); // Update the flight-passenger association
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightPassengerExists(flightPassenger.Id)) // Check if the flight-passenger exists
                    {
                        return NotFound(); // If not, return NotFound
                    }
                    else
                    {
                        throw; // If there is another issue, rethrow the exception
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index (list) page
            }

            ViewData["FlightId"] = new SelectList(_context.Set<Flight>(), "Id", "FlightNumber", flightPassenger.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Set<Passenger>(), "PassengerId", "Email", flightPassenger.PassengerId);
            return View(flightPassenger); // Return the FlightPassenger entity to the Edit view
        }

        // GET: FlightPassengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            var flightPassenger = await _context.FlightPassenger
                .Include(f => f.Flight)
                .Include(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flightPassenger == null)
            {
                return NotFound(); // If the FlightPassenger is not found, return NotFound
            }

            return View(flightPassenger); // Return the FlightPassenger details to the Delete view
        }

        // POST: FlightPassengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightPassenger = await _context.FlightPassenger.FindAsync(id);
            _context.FlightPassenger.Remove(flightPassenger); // Remove the flight-passenger relationship
            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index (list) page
        }

        private bool FlightPassengerExists(int id)
        {
            return _context.FlightPassenger.Any(e => e.Id == id); // Check if the FlightPassenger exists in the database
        }
    }
}
