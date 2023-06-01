using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurentWeb.Models.ModelDTO.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public int CateID { get; set; }
        public int Among { get; set; } = 0;
        public double? Total { get; set; } = 0;
    }
}
