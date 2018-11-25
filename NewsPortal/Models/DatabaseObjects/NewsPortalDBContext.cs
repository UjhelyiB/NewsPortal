using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3QEBAVL\\SQLEXPRESS;Initial Catalog=NewsPortal;Integrated Security=False;User Id=NewsPortalUser;Password=NewsPortalUser123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsToCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NewsToCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsToCategory_NewsCategory");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsToCategory)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsToCategory_News");
            });
        }
    }
}
