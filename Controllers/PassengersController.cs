using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FlightReservationApp_f.Controllers
{
    public class PassengersController : Controller
    {
        private readonly DataContext _context;

        // Constructor to inject the DataContext
        public PassengersController(DataContext context)
        {
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index()
        {
            // Retrieve all passengers from the database and return to the view
            return View(await _context.Passenger.ToListAsync());
        }

        // GET: Passengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            // Fetch the passenger from the database
            var passenger = await _context.Passenger
                .FirstOrDefaultAsync(m => m.PassengerId == id); // Find by PassengerId

            if (passenger == null)
            {
                return NotFound(); // If passenger not found, return NotFound
            }

            return View(passenger); // Pass the passenger object to the view
        }

        // GET: Passengers/Create
        public IActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: Passengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerId,FullName,Email,ContactNumber")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger); // Add the passenger to the context
                await _context.SaveChangesAsync(); // Save the changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index (list) of passengers
            }
            return View(passenger); // If the model state is invalid, return to the Create view
        }

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            // Fetch the passenger from the database by id
            var passenger = await _context.Passenger.FindAsync(id);
            if (passenger == null)
            {
                return NotFound(); // If passenger not found, return NotFound
            }
            return View(passenger); // Pass the passenger object to the view for editing
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassengerId,FullName,Email,ContactNumber")] Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return NotFound(); // If the id doesn't match, return NotFound
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger); // Update the passenger in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.PassengerId))  // Check if the passenger exists
                    {
                        return NotFound(); // If passenger does not exist, return NotFound
                    }
                    else
                    {
                        throw; // If there is another issue, rethrow the exception
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index page
            }
            return View(passenger); // If validation fails, return to the Edit view
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no id is passed, return NotFound
            }

            var passenger = await _context.Passenger
                .FirstOrDefaultAsync(m => m.PassengerId == id); // Fetch the passenger to delete

            if (passenger == null)
            {
                return NotFound(); // If passenger not found, return NotFound
            }

            return View(passenger); // Return the Delete view with passenger data
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passenger.FindAsync(id); // Find the passenger by id
            _context.Passenger.Remove(passenger); // Remove the passenger from the context
            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index page after deletion
        }

        // Method to check if a passenger exists by their PassengerId
        private bool PassengerExists(int id)
        {
            return _context.Passenger.Any(e => e.PassengerId == id); // Return true if passenger exists, otherwise false
        }
    }
}
