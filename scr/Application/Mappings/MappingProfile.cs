using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attachment, AttachmentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserProduct, UserProductDto>().ReverseMap();
        }
    }
}
