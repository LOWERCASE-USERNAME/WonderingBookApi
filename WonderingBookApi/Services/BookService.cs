using WonderingBookApi.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Books.v1.Data;

namespace WonderingBookApi.Services
{
    public class BookService
    {
        private readonly BooksService _booksService;
        private readonly IMapper _mapper;

        public BookService(IMapper mapper)
        {
            _booksService = new BooksService(new BaseClientService.Initializer
            {
                ApplicationName = "WonderingBookApi",
                ApiKey = "AIzaSyCD0g_aE5UqlIk25IndlejHezv9cm_GbK0"
            });
            _mapper = mapper;
        }

        public async Task<Books> GetBookAsync(string id)
        {
            var request = _booksService.Volumes.Get(id);
            var volume = await request.ExecuteAsync();

            return _mapper.Map<Books>(volume);
        }

        public async Task<IEnumerable<Books>> SearchBooksAsync(string query, int maxResults, int startIndex, string langRestrict)
        {
            var request = _booksService.Volumes.List(query);
            request.MaxResults = maxResults;
            request.StartIndex = startIndex;
            request.LangRestrict = langRestrict;
            request.PrintType = VolumesResource.ListRequest.PrintTypeEnum.BOOKS;

            var volumes = await request.ExecuteAsync();

            if(volumes == null)
            {
                throw new Exception("Error happens when trying to retrieve the volumes from API");
            }

            return _mapper.Map<IEnumerable<Books>>(volumes.Items);
        }

        public async Task<IEnumerable<Books>> SearchBooksByISBNAsync(string isbn, int maxResults, int startIndex, string langRestrict)
        {
            var request = _booksService.Volumes.List($"isbn:{isbn}");
            request.MaxResults = maxResults;
            request.StartIndex = startIndex;
            request.LangRestrict = langRestrict;
            request.PrintType = VolumesResource.ListRequest.PrintTypeEnum.BOOKS;

            var volumes = await request.ExecuteAsync();

            return _mapper.Map<IEnumerable<Books>>(volumes.Items);
        }

        public async Task<IEnumerable<Books>> SearchBooksByAuthorAsync(string author, int maxResults, int startIndex, string langRestrict)
        {
            var request = _booksService.Volumes.List($"inauthor:{author}");
            request.MaxResults = maxResults;
            request.StartIndex = startIndex;
            request.LangRestrict = langRestrict;
            request.PrintType = VolumesResource.ListRequest.PrintTypeEnum.BOOKS;

            var volumes = await request.ExecuteAsync();

            return _mapper.Map<IEnumerable<Books>>(volumes.Items);
        }

        public async Task<IEnumerable<Books>> SearchBooksByTitleAsync(string title, int maxResults, int startIndex, string langRestrict)
        {
            var request = _booksService.Volumes.List($"intitle:{title}");
            request.MaxResults = maxResults;
            request.StartIndex = startIndex;
            request.LangRestrict = langRestrict;
            request.PrintType = VolumesResource.ListRequest.PrintTypeEnum.BOOKS;
            
            var volumes = await request.ExecuteAsync();

            return _mapper.Map<IEnumerable<Books>>(volumes.Items);
        }
    }
}