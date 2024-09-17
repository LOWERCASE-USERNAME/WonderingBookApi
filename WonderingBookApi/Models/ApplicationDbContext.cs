using Microsoft.EntityFrameworkCore;

namespace WonderingBookApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTopic> ArticleTopics { get; set; }
        public DbSet<IdeaCard> IdeaCards { get; set; }
        public DbSet<SavedIdea> SavedIdeas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

        
    }
}
