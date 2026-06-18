using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationApp_f.Models
{
    [Index(nameof(User.username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Should be filled")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Should be filled")]
        [StringLength(50)]
        public string username { get; set; }

        [Required(ErrorMessage = "Should be filled")]
        public string password { get; set; }
    }
}
