﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WonderingBookApi.Models;

#nullable disable

namespace WonderingBookApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240918144139_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WonderingBookApi.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<string>("AuthorNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ArticleId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("WonderingBookApi.Models.ArticleTopic", b =>
                {
                    b.Property<int>("ArticleTopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleTopicId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("ArticleTopicId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("TopicId");

                    b.ToTable("ArticleTopics");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PageCount")
                        .HasColumnType("int");

                    b.Property<string>("PublishedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WonderingBookApi.Models.IdeaCard", b =>
                {
                    b.Property<int>("IdeaCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdeaCardId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("CardType")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdeaCardId");

                    b.HasIndex("ArticleId");

                    b.ToTable("IdeaCards");
                });

            modelBuilder.Entity("WonderingBookApi.Models.SavedIdea", b =>
                {
                    b.Property<int>("SavedIdeaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SavedIdeaId"));

                    b.Property<int>("IdeaCardId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SavedIdeaId");

                    b.HasIndex("IdeaCardId");

                    b.HasIndex("UserId");

                    b.ToTable("SavedIdeas");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("WonderingBookApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Article", b =>
                {
                    b.HasOne("WonderingBookApi.Models.Book", "Book")
                        .WithMany("Articles")
                        .HasForeignKey("BookId");

                    b.HasOne("WonderingBookApi.Models.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WonderingBookApi.Models.ArticleTopic", b =>
                {
                    b.HasOne("WonderingBookApi.Models.Article", "Article")
                        .WithMany("ArticleTopics")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WonderingBookApi.Models.Topic", "Topic")
                        .WithMany("ArticleTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("WonderingBookApi.Models.IdeaCard", b =>
                {
                    b.HasOne("WonderingBookApi.Models.Article", "Article")
                        .WithMany("IdeaCards")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("WonderingBookApi.Models.SavedIdea", b =>
                {
                    b.HasOne("WonderingBookApi.Models.IdeaCard", "IdeaCard")
                        .WithMany("SavedIdeas")
                        .HasForeignKey("IdeaCardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WonderingBookApi.Models.User", "User")
                        .WithMany("SavedIdeas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("IdeaCard");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Article", b =>
                {
                    b.Navigation("ArticleTopics");

                    b.Navigation("IdeaCards");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Book", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("WonderingBookApi.Models.IdeaCard", b =>
                {
                    b.Navigation("SavedIdeas");
                });

            modelBuilder.Entity("WonderingBookApi.Models.Topic", b =>
                {
                    b.Navigation("ArticleTopics");
                });

            modelBuilder.Entity("WonderingBookApi.Models.User", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("SavedIdeas");
                });
#pragma warning restore 612, 618
        }
    }
}
