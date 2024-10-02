using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.DTOs.IdeaCard;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class IdeaCardService : IIdeaCardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHandleFirebaseService _handleFirebaseService;

        public IdeaCardService(ApplicationDbContext context, IHandleFirebaseService handleFirebaseService)
        {
            _context = context;
            _handleFirebaseService = handleFirebaseService;
        }

        public async Task<IEnumerable<IdeaCard>> BulkCreateIdeaCardAsync(BulkCreateIdeaCardsDTO ideaCards)
        {
            var listIdeaCards = new List<IdeaCard>();
            foreach (var cardDTO in ideaCards.IdeaCards)
            {
                var ideaCard = new IdeaCard
                {
                    ArticleId = cardDTO.ArticleId,
                    CardType = cardDTO.CardType,
                    Title = cardDTO.Title,
                    Content = cardDTO.Content,
                    Order = cardDTO.Order
                };
                ideaCard.IdeaCardId = Guid.NewGuid();
                ideaCard.Image = await _handleFirebaseService.UploadImageAsync(cardDTO.Image, ideaCard.IdeaCardId);
                _context.IdeaCards.Add(ideaCard);
                listIdeaCards.Add(ideaCard);
            }

            await _context.SaveChangesAsync();
            return listIdeaCards;
        }

        public async Task<IdeaCard> CreateIdeaCardAsync(IdeaCard ideaCard)
        {
            _context.IdeaCards.Add(ideaCard);
            await _context.SaveChangesAsync();
            return ideaCard;
        }

        public async Task<IEnumerable<IdeaCard>> GetAllIdeaCardsAsync()
        {
            return await _context.IdeaCards.ToListAsync();
        }

        public async Task<IdeaCard> GetIdeaCardByIdAsync(Guid id)
        {
            var article = await _context.IdeaCards.FirstOrDefaultAsync(a => a.IdeaCardId == id);
            return article;
        }

        public async Task<IEnumerable<IdeaCard>> GetIdeaCardsByArticleAsync(Guid articleId)
        {
            return await _context.IdeaCards
                .Where(i => i.ArticleId == articleId)
                .ToListAsync();
        }

        public async Task UpdateIdeaCardAsync(IdeaCard updatedIdeaCard)
        {
            if (updatedIdeaCard == null)
                throw new ArgumentNullException(nameof(updatedIdeaCard));
            try
            {
                _context.Entry(updatedIdeaCard).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
