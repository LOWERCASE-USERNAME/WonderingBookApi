﻿using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article newArticle)
        {
            newArticle.DateCreated = DateTime.UtcNow;
            newArticle.Book = null;
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return newArticle;
        }

        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            var article = await _context.Articles.Include(article => article.IdeaCards).FirstOrDefaultAsync(a => a.ArticleId == id);
            return article;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task UpdateArticleAsync(Article updatedArticle)
        {
            if (updatedArticle == null)
                throw new ArgumentNullException(nameof(updatedArticle));
            try
            {
                _context.Entry(updatedArticle).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserIdAsync(Guid userId)
        {
            var article = await _context.Articles.Where(a => a.UserId == userId.ToString()).ToListAsync();
            return article;
        }

        public async Task<IEnumerable<Article>> GetArticlesByBookNameAsync(string name)
        {
           return await _context.Articles
                .Include(a => a.Book)
                .Where(a => a.Book.Title != null && a.Book.Title
                .Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
        public async Task<IEnumerable<Article>> RecommendArticles()
        {
            //Take random 5 articles
            var article = await _context.Articles.OrderBy(x => Guid.NewGuid()).Take(5).ToListAsync();
            return article;

        }
    }
}
