using AutoMapper;
using ManagementRestaurantLocation.Models;
using ManagementRestaurantLocation.Models.ModelDTO.CategoryDTO;
using ManagementRestaurantLocation.Models.ModelDTO.OrderDTO;
using ManagementRestaurantLocation.Models.ModelDTO.ProductDTO;
using ManagementRestaurantLocation.Models.ModelDTO.RestaurantDTO;
using ManagementRestaurantLocation.Models.RestaurantDTO;

namespace ManagementRestaurantLocation
{
    public class MapperCofi : Profile
    {
        public MapperCofi() 
        {
            CreateMap<Restaurents, RetaurantDTO>().ReverseMap();
            CreateMap<Restaurents, RetaurantCreateDTO>().ReverseMap();
            CreateMap<Restaurents, RetaurantUpdateDTO>().ReverseMap();

            CreateMap<Products, ProductDTO>().ReverseMap();
            CreateMap<Products, ProductCreatDTO>().ReverseMap();
            CreateMap<Products, ProductUpdateDTO>().ReverseMap();

            CreateMap<Categories, CategoryDTO>().ReverseMap();
            CreateMap<Categories, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Categories, CategoryCreateDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
        }

    }
}
