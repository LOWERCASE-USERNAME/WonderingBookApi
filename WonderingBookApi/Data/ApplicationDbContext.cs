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

            User user1 = new User
            {
                Id = "7a9df144-5fe3-4adb-b60c-0e309df83bd7",
                Fullname = "Tran Duc Hung (K17 HL)",
                UserName = "hungtdhe171201",
                NormalizedUserName = "HUNGTDHE171201",
                Email = "hungtdhe171201@fpt.edu.vn",
                NormalizedEmail = "HUNGTDHE171201@FPT.EDU.VN",
                EmailConfirmed = false,
                PasswordHash = null,  // Assuming password is not provided; use a hash if available
                SecurityStamp = "S75KO3CFMZXMHCVSWCS4T6JY3IEZO4LD",
                ConcurrencyStamp = "7b21a2d8-976f-4ce8-8c81-c9a9578faace",
                CreatedAt = new DateTime(2024, 10, 24, 5, 14, 16),
                ModifiedAt = new DateTime(2024, 10, 24, 5, 14, 16),
                IsAdmin = true,
                PhoneNumber = null,  // assuming no phone number is provided
            };


            User user2 = new User
            {
                Id = "4b0fe916-d76e-4182-a447-4893480c6b4c",
                Fullname = "17 Tran Hoang Giang K17",
                UserName = "giangthhe170978",
                NormalizedUserName = "GIANGTHHE170978",
                Email = "giangthhe170978@fpt.edu.vn",
                NormalizedEmail = "GIANGTHHE170978@FPT.EDU.VN",
                EmailConfirmed = false,
                PasswordHash = null,  // Assuming password is not provided; use a hash if available
                SecurityStamp = "QVQX4RBEDDPFMPWUJYH45JH7YJFFTAOS",
                ConcurrencyStamp = "8da8cab1-ee6d-4d24-9984-001ef8139ea3",
                CreatedAt = new DateTime(2024, 10, 25, 13, 41, 01),
                ModifiedAt = new DateTime(2024, 10, 25, 13, 41, 01),
                IsAdmin = true,
                PhoneNumber = null,  // assuming no phone number is provided
            };


            User user3 = new User
            {
                Id = "3d006212-a50c-45d5-9368-b8d3c0548de3",
                Fullname = "Bùi Gia Khánh",
                UserName = "khanhgiahaika3",
                NormalizedUserName = "KHANHGIAHAIKA3",
                Email = "khanhgiahaika3@gmail.com",
                NormalizedEmail = "KHANHGIAHAIKA3@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEOInVfBvutBd8S8U2Ck8h2U3RHdD4EcHixqFx4djMr/io5hJ2kHThCmZ4R+gyZZWQw==",
                SecurityStamp = "5LVH7X6TASZOU4LISF6OD32DCGAIE52J",
                ConcurrencyStamp = "17b82427-bb11-458e-a9bd-96bd0f8ce73f",
                CreatedAt = new DateTime(2024, 10, 23, 1, 57, 10),
                ModifiedAt = new DateTime(2024, 10, 23, 1, 57, 10),
                IsAdmin = false,
                PhoneNumber = null,  // assuming no phone number is provided
            };

            User user4 = new User
            {
                Id = "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd",
                Fullname = "Trần Quyết Chiến",
                UserName = "chienquyetsthang",
                NormalizedUserName = "CHIENQUETSTHANG",
                EmailConfirmed = false,
                Email = "chienquyetsthang@gmail.com",
                NormalizedEmail = "CHIENQUETSTHANG@GMAIL.COM",
                ConcurrencyStamp = "0ba16f7a-6406-4dea-b0a2-98252ba9180f",
                SecurityStamp = "FQ3GKA3ESFQ65WY6COZSY2NUNMJBJJCK",
                PasswordHash = "AQAAAAIAAYagAAAAECxtILI8M8NNbIKk1SARv2l8niXqUXeHAjdd+U6s3Z88PI671uQL0vCJdgOhACDgBQ==",
                CreatedAt = new DateTime(2024, 10, 26, 13, 56, 56),
                ModifiedAt = new DateTime(2024, 10, 26, 13, 56, 56),
                IsAdmin = false,
                PhoneNumber = null,  // assuming no phone number is provided
            };

            User user5 = new User
            {
                Id = "41d26d4e-d471-4e9c-b1fc-8b01e73218b3",
                Fullname = "Trần Huy Hoàng",
                UserName = "hoangtran8386",
                NormalizedUserName = "HOANGTRAN8386",
                Email = "hoangtran8386@gmail.com",
                NormalizedEmail = "HOANGTRAN8386@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEJCRqT2rhrpnh0B7xqxFZTFijjbXl/4i3/jQKkrr6uyIdYcLk67oAUdRmOvmkUdBnw==",
                SecurityStamp = "JFIALES6WXZYQRDRMSS4CUBPEM2ALTXJ",
                ConcurrencyStamp = "072a38e7-3f28-40a3-b37f-278543217c7f",
                CreatedAt = new DateTime(2024, 10, 22, 14, 45, 19),
                ModifiedAt = new DateTime(2024, 10, 22, 14, 45, 19),
                IsAdmin = false,
                PhoneNumber = null,  // Assuming no phone number is provided
            };

            User user6= new User
            {
                Id = "3fc33030-5b15-481a-9ca6-ebb458b0e08c",
                Fullname = "Hoàng Huy Tuấn",
                UserName = "tuanhoang333",
                NormalizedUserName = "TUANHOANG333",
                Email = "tuanhoang333@gmail.com",
                NormalizedEmail = "TUANHOANG333@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEIRCZ/X2WM79M5kVq9c3L15pZrEvN/TTUEQ+H++Wd+gNuewMropdS1BJ47imojZR+Q==",
                SecurityStamp = "M5WLBKZOVBWHW2VWBW7IVYOPTFM7UIJR",
                ConcurrencyStamp = "404ea804-cbb9-4086-8f5a-25a77be98934",
                CreatedAt = new DateTime(2024, 10, 21, 3, 32, 31),
                ModifiedAt = new DateTime(2024, 10, 21, 3, 32, 47),
                IsAdmin = false,
                PhoneNumber = null,  // assuming no phone number is provided
            };

            // user 1 2 = admin, 3 moderator, 4 content provider, 5 premium user, 6 normal user
            modelBuilder.Entity<User>().HasData(user1, user2, user3, user4, user5, user6);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1bfa4d50-b885-48b9-835a-f47ad854046b", UserId = "7a9df144-5fe3-4adb-b60c-0e309df83bd7" },
                new IdentityUserRole<string> { RoleId = "1bfa4d50-b885-48b9-835a-f47ad854046b", UserId = "4b0fe916-d76e-4182-a447-4893480c6b4c" },
                new IdentityUserRole<string> { RoleId = "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b", UserId = "3d006212-a50c-45d5-9368-b8d3c0548de3" },
                new IdentityUserRole<string> { RoleId = "f1de4cb5-7f7c-4ba9-9453-22ec3840984b", UserId = "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd" },
                new IdentityUserRole<string> { RoleId = "2281c643-65a2-4fd6-83bc-ae240794b875", UserId = "41d26d4e-d471-4e9c-b1fc-8b01e73218b3" },
                new IdentityUserRole<string> { RoleId = "fcd4a4b5-d492-4290-8c45-8c94b7f8d689", UserId = "3fc33030-5b15-481a-9ca6-ebb458b0e08c" }
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
