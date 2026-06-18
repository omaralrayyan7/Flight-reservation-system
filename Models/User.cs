
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
    using System.ComponentModel.DataAnnotations;

    namespace FlightReservationApp_f.Models
{
        [Index(nameof(User.username), IsUnique = true)]
        public class User
        {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Should be insert")]
            [StringLength(50)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Should be insert")]
            [StringLength(50)]
            public string username { get; set; }

            [Required(ErrorMessage = "Should be insert")]
            public string password { get; set; }

            
        }
    }

