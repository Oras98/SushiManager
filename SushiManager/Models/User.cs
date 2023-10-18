using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [DisplayName("Ruolo")]
        public string Role { get; set; }
    }
}
