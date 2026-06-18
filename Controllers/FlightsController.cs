using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Data;
using FlightReservationApp_f.Models;

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
            ViewData["FlightNumberSortParm"] = string.IsNullOrEmpty(sortOrder) ? "flightNumber_desc" : "";
            ViewData["OriginSortParm"] = sortOrder == "Origin" ? "origin_desc" : "Origin";
            ViewData["DestinationSortParm"] = sortOrder == "Destination" ? "destination_desc" : "Destination";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            var flights = from f in _context.Flight select f;

            if (!string.IsNullOrEmpty(flightNumberSearch))
                flights = flights.Where(f => f.FlightNumber.Contains(flightNumberSearch));

            switch (sortOrder)
            {
                case "flightNumber_desc": flights = flights.OrderByDescending(f => f.FlightNumber); break;
                case "Price":            flights = flights.OrderBy(f => f.Price); break;
                case "price_desc":       flights = flights.OrderByDescending(f => f.Price); break;
                case "Origin":           flights = flights.OrderBy(f => f.Origin); break;
                case "origin_desc":      flights = flights.OrderByDescending(f => f.Origin); break;
                case "Destination":      flights = flights.OrderBy(f => f.Destination); break;
                case "destination_desc": flights = flights.OrderByDescending(f => f.Destination); break;
                default:                 flights = flights.OrderBy(f => f.FlightNumber); break;
            }

            ViewData["flightNumberSearch"] = flightNumberSearch;
            return View(await flights.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var flight = await _context.Flight
                .Include(f => f.FlightPassengers)
                .ThenInclude(f => f.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null) return NotFound();
            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create() => View();

        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightNumber,DepartureTime,ArrivalTime,Origin,Destination,SeatCapacity,Type,DepartureLocation,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var flight = await _context.Flight.FindAsync(id);
            if (flight == null) return NotFound();
            return View(flight);
        }

        // POST: Flights/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNumber,DepartureTime,ArrivalTime,Origin,Destination,SeatCapacity,Type,DepartureLocation,Price")] Flight flight)
        {
            if (id != flight.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var flight = await _context.Flight.FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null) return NotFound();
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null) _context.Flight.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id) => _context.Flight.Any(e => e.Id == id);
    }
}
