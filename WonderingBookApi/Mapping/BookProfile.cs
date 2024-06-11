using AutoMapper;
using Google.Apis.Books.v1.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile() {
            CreateMap<Volume, Books>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.VolumeInfo.Title))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => string.Join(", ", src.VolumeInfo.Authors)))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.VolumeInfo.Publisher))
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.VolumeInfo.PublishedDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.VolumeInfo.Description))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.VolumeInfo.IndustryIdentifiers != null
                    && src.VolumeInfo.IndustryIdentifiers.Any() ? src.VolumeInfo.IndustryIdentifiers.First().Identifier : null))
                .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.VolumeInfo.PageCount))
                .ForMember(dest => dest.ImageLink, opt => opt.MapFrom(src => src.VolumeInfo.ImageLinks.Thumbnail));
        }
    }
}
