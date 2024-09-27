using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class IdeaCardService : IIdeaCardService
    {
        private readonly ApplicationDbContext _context;

        public IdeaCardService(ApplicationDbContext context)
        {
            _context = context;
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
