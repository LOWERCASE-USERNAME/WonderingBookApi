using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.DTOs.Article
{
    public class UpdateArticleStatusDTO
    {
        public Guid ArticleId { get; set; }
        public ArticleStatus? Status { get; set; }
    }
}
