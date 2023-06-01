using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurentWeb.Models.ModelDTO.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        public int RetaurentID { get; set; }
        public Restaurents Restaurent { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public int? Status { get; set; }

        public string? Image { get; set; }
    }
}
