using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ------------------------- 
            // Table Articles 
            // -------------------------
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("articles");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Id)
                .HasColumnName("id");

                entity.Property(a => a.Title)
                .HasColumnName("title")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(a => a.Content)
                .HasColumnName("content")
                .IsRequired();

                entity.Property(a => a.CreatedAt)
    .HasColumnName("created_at")
    .HasColumnType("timestamp")
    .ValueGeneratedOnAdd()
    .HasDefaultValueSql("CURRENT_TIMESTAMP");


                entity.Property(a => a.UpdatedAt)
                .HasColumnName("updated_at");

                //entity.HasMany(a => a.Comments)
                //.WithOne(c => c.Article)
                //.HasForeignKey(c => c.ArticleId)
                //.OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------- 
            // Table Comments 
            // -------------------------
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                .HasColumnName("id");

                entity.Property(c => c.ArticleId)
                .HasColumnName("article_id")
                .IsRequired();

                entity.Property(c => c.Author)
                .HasColumnName("author")
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(c => c.Content)
                .HasColumnName("content")
                .IsRequired();

                entity.Property(c => c.CreatedAt)
    .HasColumnName("created_at")
    .HasColumnType("timestamp")
    .ValueGeneratedOnAdd()
    .HasDefaultValueSql("CURRENT_TIMESTAMP");


                // Relation FK : Comment → Article
                //entity.HasOne(c => c.Article)
                //.WithMany(a => a.Comments)
                //.HasForeignKey(c => c.ArticleId)
                //.OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}