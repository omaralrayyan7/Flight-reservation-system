using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    public class Passenger
    {
        public int PassengerId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string ContactNumber { get; set; }

        public IEnumerable<FlightPassenger>? FlightsPassenger { get; set; }
    }
}
