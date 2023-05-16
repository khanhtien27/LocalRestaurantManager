using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurantLocation.Models.ModelDTO.OrderDTO
{
    public class OderUpdateDTO
    {
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public int CateID { get; set; }
        public int Among { get; set; }
        public double? Total { get; set; }
    }
}
