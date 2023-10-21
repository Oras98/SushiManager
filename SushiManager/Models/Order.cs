using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]        
        public User? User { get; set; }

        [Required]
        private List<OrderDetail> Details { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? SubmitDate { get; set; }
    }
}
