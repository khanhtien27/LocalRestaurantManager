using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurentWeb.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }

        public DateTime? Create_At { get; set; }
        public DateTime? Update_At { get; set; }

        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public int CateID { get; set; }
        public int Among { get; set; }
        public double? Total { get; set; }
    }
}
