using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    public class FlightPassenger
    {
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }

        [Required]
        public int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
