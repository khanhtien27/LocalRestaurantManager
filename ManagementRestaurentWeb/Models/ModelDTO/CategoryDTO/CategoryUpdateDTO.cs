using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurantLocation.Models.ModelDTO.CategoryDTO
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public int? Status { get; set; }

        public double Price { get; set; }
        public double? PriceSale { get; set; }

        public string? Image { get; set; }
    }
}
