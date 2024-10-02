using WonderingBookApi.DTOs.IdeaCard;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IIdeaCardService
    {
        Task<IdeaCard> CreateIdeaCardAsync(IdeaCard ideaCard);
        Task<IEnumerable<IdeaCard>> BulkCreateIdeaCardAsync(BulkCreateIdeaCardsDTO ideaCards);
        Task<IdeaCard> GetIdeaCardByIdAsync(Guid id);
        Task<IEnumerable<IdeaCard>> GetAllIdeaCardsAsync();
        Task<IEnumerable<IdeaCard>> GetIdeaCardsByArticleAsync(Guid articleId);
        Task UpdateIdeaCardAsync(IdeaCard updatedIdeaCard);
        // Task DeleteIdeaCardAsync(Guid id);
    }
}
