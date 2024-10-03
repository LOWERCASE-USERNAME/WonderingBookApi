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
                if(cardDTO.Image != null)
                {
                    ideaCard.Image = await _handleFirebaseService.UploadImageAsync(cardDTO.Image, ideaCard.IdeaCardId);
                }
                _context.IdeaCards.Add(ideaCard);
                listIdeaCards.Add(ideaCard);
            }

            await _context.SaveChangesAsync();
            return listIdeaCards;
        }

        public async Task<IEnumerable<IdeaCard>> BulkUpdateIdeaCardAsync(BulkUpdateIdeaCardsDTO ideaCards)
        {
            var listIdeaCards = new List<IdeaCard>();

            foreach (var cardDTO in ideaCards.IdeaCards)
            {
                var existingIdeaCard = await _context.IdeaCards.FindAsync(cardDTO.IdeaCardId);

                if (existingIdeaCard == null)
                {
                    // If the record is missing, skip it (or handle it accordingly)
                    Console.WriteLine($"IdeaCard with ID {cardDTO.IdeaCardId} not found.");
                    continue;
                }

                // Now create and attach the new IdeaCard instance
                existingIdeaCard.ArticleId = cardDTO.ArticleId;
                existingIdeaCard.CardType = cardDTO.CardType;
                existingIdeaCard.Title = cardDTO.Title;
                existingIdeaCard.Content = cardDTO.Content;
                existingIdeaCard.Image = cardDTO.Image;
                existingIdeaCard.Order = cardDTO.Order;


                //_context.IdeaCards.Attach(ideaCard);  // Attach new entity
                _context.Entry(existingIdeaCard).State = EntityState.Modified;  // Mark it as modified

                listIdeaCards.Add(existingIdeaCard);
            }

            await _context.SaveChangesAsync();  // Save changes to the database
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
