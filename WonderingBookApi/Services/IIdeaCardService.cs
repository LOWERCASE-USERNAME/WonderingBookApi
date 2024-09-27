using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IIdeaCardService
    {
        Task<IdeaCard> CreateIdeaCardAsync(IdeaCard ideaCard);
        Task<IdeaCard> GetIdeaCardByIdAsync(Guid id);
        Task<IEnumerable<IdeaCard>> GetAllIdeaCardsAsync();
        Task UpdateIdeaCardAsync(IdeaCard updatedIdeaCard);
        // Task DeleteIdeaCardAsync(Guid id);
    }
}
