using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FlightReservationApp_f.Controllers
{
    public class FlightsController : Controller
    {
        private readonly DataContext _context;

        public FlightsController(DataContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(string flightNumberSearch, string sortOrder)
        {
            // Sorting Links in ViewData
            ViewData["FlightNumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "flightNumber_desc" : "";
            ViewData["OriginSortParm"] = sortOrder == "Origin" ? "origin_desc" : "Origin";
            ViewData["DestinationSortParm"] = sortOrder == "Destination" ? "destination_desc" : "Destination";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            var flights = from f in _context.Flight
                          select f;

            // Filter by flight number search term
            if (!string.IsNullOrEmpty(flightNumberSearch))
            {
                flights = flights.Where(f => f.FlightNumber.Contains(flightNumberSearch));
            }

            // Sorting logic based on selected column
            switch (sortOrder)
            {
                case "flightNumber_desc":
                    flights = flights.OrderByDescending(f => f.FlightNumber);
                    break;
                case "Price":
                    flights = flights.OrderBy(f => f.Price);
                    break;
                case "price_desc":
                    flights = flights.OrderByDescending(f => f.Price);
                    break;
                case "Origin":
                    flights = flights.OrderBy(f => f.Origin);
                    break;
                case "origin_desc":
                    flights = flights.OrderByDescending(f => f.Origin);
                    break;
                case "Destination":
                    flights = flights.OrderBy(f => f.Destination);
                    break;
                case "destination_desc":
                    flights = flights.OrderByDescending(f => f.Destination);
                    break;
                default:
                    flights = flights.OrderBy(f => f.FlightNumber);
                    break;
            }

            // Passing the flightNumberSearch value to the view for persistence across requests
            ViewData["flightNumberSearch"] = flightNumberSearch;

            return View(await flights.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.Include(f => f.FlightPassengers).ThenInclude(f => f.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightNumber,DepartureTime,ArrivalTime,Origin,Destination,SeatCapacity,Type,DepartureLocation,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Redirect to the Index page after saving
            }
            return View(flight);  // Return the view if the model is invalid
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);  // Return the view with the flight to edit
        }

        // POST: Flights/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNumber,DepartureTime,ArrivalTime,Origin,Destination,SeatCapacity,Type,DepartureLocation,Price")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();  // Ensure that the flight IDs match
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);  // Update the flight in the context
                    await _context.SaveChangesAsync();  // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    // If a concurrency issue happens or the flight no longer exists
                    if (!FlightExists(flight.Id))
                    {
                        return NotFound();  // If the flight doesn't exist anymore, return not found
                    }
                    else
                    {
                        throw;  // Otherwise, throw the exception for further handling
                    }
                }

                return RedirectToAction(nameof(Index));  // Redirect to the Index page to show updated data
            }

            return View(flight);  // Return the view with validation errors if the model is invalid
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);  // Return the view with the flight to delete
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flight.FindAsync(id);
            _context.Flight.Remove(flight);  // Remove the flight from the database
            await _context.SaveChangesAsync();  // Save changes to the database
            return RedirectToAction(nameof(Index));  // Redirect to the Index page after deleting
        }

        // Private helper method to check if the flight exists
        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.Id == id);  // Check if the flight exists in the database
        }
    }
}
