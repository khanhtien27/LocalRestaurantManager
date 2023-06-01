using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurentWeb.Models
{
    public class Restaurents
    {
        [Key]
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
        public DateTime? Creat_At { get; set; }
        public DateTime? Update_At { get; set; }
    }
}
