using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurantLocation.Models.ModelDTO.ProductDTO
{
    public class ProductUpdateDTO
    {
        [Required]
        public int RetaurentID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public int? Status { get; set; }

        public string? Image { get; set; }
    }
}
