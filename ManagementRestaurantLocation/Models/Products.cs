using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementRestaurantLocation.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurent")]
        public int RetaurentID { get; set; }

        public Restaurents Restaurent { get; set;}

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public int? Status { get; set; }

        [Required]
        public string Image { get; set; }
        public DateTime? Creat_At { get; set; }
        public DateTime? Update_At { get; }

    }
}
