using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    [Index(nameof(Admin.username), IsUnique = true)]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string Name { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
