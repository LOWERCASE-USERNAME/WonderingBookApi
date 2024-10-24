using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckExistAsync(string id) => await _context.Books.AnyAsync(b => b.Id == id);

        public async Task<Book> CreateBookAsync(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksByNameAsync(string name)
        {
            return await _context.Books
                 .Where(b => b.Title != null && b.Title.ToLower().Contains(name.ToLower()))
                 .ToListAsync();
        }
    }
}
