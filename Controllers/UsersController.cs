using FlightReservationApp_f.Models;
using FlightReservationApp_f.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationApp_f.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            bool empty = CheckEmpty(user);
            bool duplicate = CheckUsername(user.username);

            if (empty)
            {
                if (duplicate)
                {
                    _context.User.Add(user);
                    _context.SaveChanges();
                    TempData["Msg"] = "Registration successful!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Msg"] = "Username already taken.";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all fields.";
                return View();
            }
        }

        private bool CheckUsername(string username)
        {
            var user = _context.User.Where(u => u.username.Equals(username)).FirstOrDefault();
            return user == null; // true = username is available
        }

        private bool CheckEmpty(User user)
        {
            if (string.IsNullOrEmpty(user.username)) return false;
            if (string.IsNullOrWhiteSpace(user.username)) return false;
            if (string.IsNullOrEmpty(user.password)) return false;
            return true;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(Login userlogin)
        {
            if (ModelState.IsValid)
            {
                string username = userlogin.username;
                string password = userlogin.password;

                User? user = _context.User
                    .Where(u => u.username.Equals(username) && u.password.Equals(password))
                    .FirstOrDefault();

                Admin? admin = _context.Admin
                    .Where(u => u.username.Equals(username) && u.password.Equals(password))
                    .FirstOrDefault();

                if (user != null)
                    return RedirectToAction("Index", "Flights");
                else if (admin != null)
                    return RedirectToAction("Index", "Flights");
                else
                    TempData["Msg"] = "Invalid username or password.";
            }
            return View();
        }

        public IActionResult TeamList() => View();
    }
}
