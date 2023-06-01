using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO
{
    public class RetaurantDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        [Required]
        public string Address { get; set; }
        public int Rate { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
