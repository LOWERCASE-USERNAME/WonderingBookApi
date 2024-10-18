using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IBookService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newBook"></param>
        /// <returns></returns>
        Task<Book> CreateBookAsync(Book newBook);
        Task<bool> CheckExistAsync(string id);
        Task<IEnumerable<Book>> GetBooksAsync();

    }
}
