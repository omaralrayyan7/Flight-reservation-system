using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    public class FlightPassenger
    {
        // Primary key for the FlightPassenger entity
        public int Id { get; set; }

        // Foreign key to the Flight entity
        [Required]
        public int FlightId { get; set; }

        // Navigation property for the related Flight
        public Flight? Flight { get; set; }

        // Foreign key to the Passenger entity
        [Required]
        public int PassengerId { get; set; }

        // Navigation property for the related Passenger
        public Passenger? Passenger { get; set; }
    }
}
