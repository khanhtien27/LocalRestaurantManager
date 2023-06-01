using AutoMapper;
using ManagementRestaurentWeb.Models;
using ManagementRestaurentWeb.Models.ModelDTO.CategoryDTO;
using ManagementRestaurentWeb.Models.ModelDTO.OrderDTO;
using ManagementRestaurentWeb.Models.ModelDTO.ProductDTO;
using ManagementRestaurentWeb.Models.ModelDTO.RestaurantDTO;

namespace ManagementRestaurentWeb
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
