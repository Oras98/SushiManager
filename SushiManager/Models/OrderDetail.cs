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
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]        
        [DisplayName("ID prodotto")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [DisplayName("Info prodotto")]
        public Product Product { get; set; }

        [Range(1,10, ErrorMessage = "La quantità deve essere tra 1 e 10 !")]
        [DisplayName("Quantità")]
        public int Quantity { get; set; } = 0;
        
    }
}
