using WonderingBookApi.DTOs.IdeaCard;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IIdeaCardService
    {
        Task<IdeaCard> CreateIdeaCardAsync(IdeaCard ideaCard);
        Task<IEnumerable<IdeaCard>> BulkCreateIdeaCardAsync(BulkCreateIdeaCardsDTO ideaCards);
        Task<IEnumerable<IdeaCard>> BulkUpdateIdeaCardAsync(BulkUpdateIdeaCardsDTO ideaCards);
        Task<IdeaCard> GetIdeaCardByIdAsync(Guid id);
        Task<IEnumerable<IdeaCard>> GetAllIdeaCardsAsync();
        Task<IEnumerable<IdeaCard>> GetIdeaCardsByArticleAsync(Guid articleId);
        Task UpdateIdeaCardAsync(IdeaCard updatedIdeaCard);
        Task DeleteIdeaCardsAsync(List<Guid> ideaCards);
    }
}
