//using BloggerAndCms.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BloggerAndCms.Data
//{
//    public class AppDbContext: DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//        {
//        }

//        public DbSet<User> Users { get; set; }
//        public DbSet<BlogPost> BlogPosts { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<SEOData> SEOData { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // SEOData one-to-one with BlogPost
//            modelBuilder.Entity<SEOData>()
//                .HasOne(s => s.BlogPost)
//                .WithOne()
//                .HasForeignKey<SEOData>(s => s.BlogPostId);
//        }



//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Configure many-to-many relationship
//            modelBuilder.Entity<BlogPost>()
//                .HasMany(p => p.Categories)
//                .WithMany(c => c.BlogPosts)
//                .UsingEntity<Dictionary<string, object>>(
//                    "BlogPostCategories",
//                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
//                    j => j.HasOne<BlogPost>().WithMany().HasForeignKey("BlogPostId"),
//                    j => j.HasKey("BlogPostId", "CategoryId"));
//        }
//    }
//}

using BloggerAndCms.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggerAndCms.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BlogPost <-> Category many-to-many
            modelBuilder.Entity<BlogPost>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.BlogPosts)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostCategories",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    j => j.HasOne<BlogPost>().WithMany().HasForeignKey("BlogPostId"),
                    j => j.HasKey("BlogPostId", "CategoryId")
                );

            
        }
    }
}

