using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    public enum FlightType
    {
        Domestic,
        International
    }

    public class Flight
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Range(0, 500)]
        public int SeatCapacity { get; set; }

        [EnumDataType(typeof(FlightType))]
        public FlightType Type { get; set; }

        [Required]
        public string DepartureLocation { get; set; }

        public decimal Price { get; set; }

        // Navigation property for many-to-many with Passenger
        public IEnumerable<FlightPassenger>? FlightPassengers { get; set; }
    }
}
