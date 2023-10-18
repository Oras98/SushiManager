using SushiRestaurant.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Product")]
        [DisplayName("ID prodotto")]
        public int ProductId { get; set; }

        [Range(1,10, ErrorMessage = "La quantità deve essere tra 1 e 10 !")]
        [DisplayName("Quantità")]
        public int Quantity { get; set; } = 0;

        [NotMapped]
        public string ProductName { get; set; }
    }
}
