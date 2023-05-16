using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurantLocation.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public Products Product { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public int? Status { get; set; }

        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }

        [Required]
        public string Image { get; set; }
        public DateTime? Creat_At { get; set; }
        public DateTime? Update_At { get; }
    }
}
