using System.ComponentModel.DataAnnotations;
namespace FlightReservationApp_f.Models
{
        public class Login
        {
            [Required(ErrorMessage = "*")]
            public string username { get; set; }

            [Required(ErrorMessage = "*")]

            public string password { get; set; }
        }
    }

