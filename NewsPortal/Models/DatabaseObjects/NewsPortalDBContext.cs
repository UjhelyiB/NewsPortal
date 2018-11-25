using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace NewsPortal.Models.DatabaseObjects
{
    public partial class NewsPortalDBContext : DbContext
    {
        public NewsPortalDBContext()
        {
        }

        public NewsPortalDBContext(DbContextOptions<NewsPortalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsToCategory> NewsToCategory { get; set; }
        public virtual DbSet<Writers> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("NewsPortalDatabase");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NewsToCategory>(entity =>
            //{
            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.NewsToCategory)
            //        .HasForeignKey(d => d.CategoryId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_NewsToCategory_NewsCategory");

            //    entity.HasOne(d => d.News)
            //        .WithMany(p => p.NewsToCategory)
            //        .HasForeignKey(d => d.NewsId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_NewsToCategory_News");
            //});
        }
    }
}
