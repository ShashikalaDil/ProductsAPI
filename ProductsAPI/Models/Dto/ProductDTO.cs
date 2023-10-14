using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 


    }
}
