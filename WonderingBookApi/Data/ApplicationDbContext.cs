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

            //// Seeding data for Position table
            //modelBuilder.Entity<Topic>().HasData(
            //    new Topic { TopicName = "habit" },
            //    new Topic { TopicName = "productivity" },
            //    new Topic { TopicName = "mindfulness" },
            //    new Topic { TopicName = "motivation" },
            //    new Topic { TopicName = "personal-development" },
            //    new Topic { TopicName = "success" },
            //    new Topic { TopicName = "growth" },
            //    new Topic { TopicName = "learning" },
            //    new Topic { TopicName = "inspiration" },
            //    new Topic { TopicName = "wellness" }
            //    );
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
