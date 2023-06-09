﻿using System.ComponentModel.DataAnnotations;

namespace ManagementRestaurantLocation.Models.ModelDTO.RestaurantDTO
{
    public class RetaurantCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public string Address { get; set; }
        public int Rate { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
