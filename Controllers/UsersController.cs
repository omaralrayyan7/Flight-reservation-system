using FlightReservationApp_f.Models;
using FlightReservationApp_f.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightReservationApp_f.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            bool empty = checkEmpty(user);
            bool duplicate = checkUsername(user.username);

            if (empty)
            {
                if (duplicate)
                {
                    _context.User.Add(user);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return View();
                }
                else
                {
                    TempData["Msg"] = "please fill the username";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "please fill all input";
                return View();
            }
        }
        public bool checkUsername(string username)
        {
            User user = _context.User.Where(u => u.username.Equals(username)).FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool checkEmpty(User user)
        {
            if (String.IsNullOrEmpty(user.username)) return false;
            else if (String.IsNullOrEmpty(user.username.Trim())) return false;
            else return true;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login userlogin)
        {
            if (ModelState.IsValid)
            {
                string username = userlogin.username;
                string password = userlogin.password;
                User user = _context.User.Where(
                      u => u.username.Equals(username) &&
                      u.password.Equals(password)
                      ).FirstOrDefault();

                Admin admin = _context.Admin.Where(
                    u => u.username.Equals(username) &&
                    u.password.Equals(password)
                    ).FirstOrDefault();


                if (user != null)
                {
                    return RedirectToAction("");
                }
                else if (admin != null)
                {

                    return RedirectToAction("f");
                }
            }
            else
            {
                TempData["Msg"] = "The user not found";
            }
            return View();
        }

        public IActionResult TeamList()
        {
            return View();
        }

    }
}