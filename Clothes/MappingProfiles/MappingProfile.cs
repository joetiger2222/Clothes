using AutoMapper;
using Clothes.DTO;
using Clothes.Models;

namespace Clothes.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ClotheItem,ClothItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category,CreateNewCategoryDto>().ReverseMap();
            CreateMap<ClotheItem, CreateNewClothItemDto>().ReverseMap();
            CreateMap<ClotheItem, UpdateClothItemDto>().ReverseMap();
            CreateMap<User,RegisterRequestDto>().ReverseMap();
            CreateMap<Cart,AddToCartDto>().ReverseMap();
            CreateMap<Cart,CartDto>().ReverseMap();
            CreateMap<Cart,UpdateCartItemDto>().ReverseMap();
        }
    }
}
