using AutoMapper;
using WonderingBookApi.DTOs.Article;
using WonderingBookApi.Models;

namespace WonderingBookApi.Mapping
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile() 
        {
            // Map from source to dest
            CreateMap<CreateArticleDTO, Article>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<UpdateArticleDTO, Article>();
            CreateMap<Article, UpdateArticleDTO>();
        }
        
    }
}
