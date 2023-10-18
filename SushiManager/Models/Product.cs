using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrizione")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Ingredienti")]
        public string Ingredients { get; set; }
    }
}
