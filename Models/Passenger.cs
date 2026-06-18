using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    public class Passenger
    {
        // Primary key for the Passenger entity
        public int PassengerId { get; set; }

        // Full Name with validation (required and custom error message)
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        // Email address with validation (required and correct email format)
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        // Contact Number with validation (required and valid phone number format)
        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string ContactNumber { get; set; }

        // Navigation property to the FlightPassenger table for many-to-many relationship
        public IEnumerable<FlightPassenger>? FlightsPassenger { get; set; }
    }
}
