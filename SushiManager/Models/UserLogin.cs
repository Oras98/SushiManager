using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.Models
{
    [NotMapped]
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
