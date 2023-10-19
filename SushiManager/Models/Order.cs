using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public List<OrderDetail> Details { get; set; }
    }
}
