using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Models;

namespace WonderingBookApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            // Unique constraint for TransactionCode
            modelBuilder.Entity<FinancialTransaction>()
                .HasIndex(w => w.TransactionCode)
                .IsUnique();

            // Define one-to-one relationship between User and Wallet
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId); 

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);

            // Seeding data for Position table 
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1bfa4d50-b885-48b9-835a-f47ad854046b", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b", Name = "Moderator", NormalizedName = "MODERATOR" },
                new IdentityRole { Id = "f1de4cb5-7f7c-4ba9-9453-22ec3840984b", Name = "ContentProvider", NormalizedName = "CONTENTPROVIDER" },
                new IdentityRole { Id = "2281c643-65a2-4fd6-83bc-ae240794b875", Name = "PremiumUser", NormalizedName = "PREMIUMUSER" },
                new IdentityRole { Id = "fcd4a4b5-d492-4290-8c45-8c94b7f8d689", Name = "RegularUser", NormalizedName = "REGULARUSER" }
                );

            // Seeding data for Position table
            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = 1 , TopicName = "habit" },
                new Topic { TopicId = 2, TopicName = "productivity" },
                new Topic { TopicId = 3, TopicName = "mindfulness" },
                new Topic { TopicId = 4, TopicName = "motivation" },
                new Topic { TopicId = 5, TopicName = "personal-development" },
                new Topic { TopicId = 6, TopicName = "success" },
                new Topic { TopicId = 7, TopicName = "growth" },
                new Topic { TopicId = 8, TopicName = "learning" },
                new Topic { TopicId = 9, TopicName = "inspiration" },
                new Topic { TopicId = 10, TopicName = "wellness" }
                );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTopic> ArticleTopics { get; set; }
        public DbSet<IdeaCard> IdeaCards { get; set; }
        public DbSet<SavedIdea> SavedIdeas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
