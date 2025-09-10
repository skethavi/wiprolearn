// Controllers/BlogPostsController.cs
using BloggerAndCms.Data;
using BloggerAndCms.DTOs;
using BloggerAndCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BlogPostsController> _logger;

        public BlogPostsController(AppDbContext context, ILogger<BlogPostsController> logger)
        {
            _context = context;
            //_logger = logger;
            _logger = logger;
        }


        //changed code



        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogPosts()
        {
            try
            {
                var blogPosts = await _context.BlogPosts
                    .Include(p => p.Author)
                    .Include(p => p.Categories)
                    .Where(p => p.Status == "Published")
                    .OrderByDescending(p => p.PublishedAt)
                    .Select(p => new
                    {
                        p.Id,
                        p.Title,
                        p.Content,
                        Excerpt = p.Excerpt ?? string.Empty,
                        Slug = p.Slug ?? string.Empty,
                        FeaturedImageUrl = p.FeaturedImageUrl ?? string.Empty,
                        p.Status,
                        p.PublishedAt,
                        p.CreatedAt,
                        p.UpdatedAt,
                        MetaTitle = p.MetaTitle ?? string.Empty,
                        MetaDescription = p.MetaDescription ?? string.Empty,
                        Tags = p.Tags ?? string.Empty,
                        Author = p.Author != null ? new
                        {
                            p.Author.Id,
                            Username = p.Author.Username ?? string.Empty,
                            Email = p.Author.Email ?? string.Empty
                        } : new { Id = 0, Username = "Unknown", Email = "" },
                        Categories = p.Categories.Select(c => new
                        {
                            c.Id,
                            Name = c.Name ?? string.Empty,
                            Slug = c.Slug ?? string.Empty
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog posts");
                return StatusCode(500, "Internal server error: Unable to retrieve blog posts");
            }
        }

        // GET: api/BlogPosts/5 (Public - for guests)
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id && p.Status == "Published");

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

        // GET: api/BlogPosts/user (For authenticated users)
        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetUserBlogPosts()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _context.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Categories)
                .Where(p => p.AuthorId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        // POST: api/BlogPosts
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPostDto blogPostDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                Excerpt = blogPostDto.Excerpt,
                Slug = blogPostDto.Slug,
                FeaturedImageUrl = blogPostDto.FeaturedImageUrl,
                AuthorId = userId,
                Status = blogPostDto.Status,
                MetaTitle = blogPostDto.MetaTitle,
                MetaDescription = blogPostDto.MetaDescription,
                Tags = blogPostDto.Tags,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (blogPostDto.Status == "Published")
            {
                blogPost.PublishedAt = DateTime.UtcNow;
            }

            // Add categories
            if (blogPostDto.CategoryIds.Any())
            {
                var categories = await _context.Categories
                    .Where(c => blogPostDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                blogPost.Categories = categories;
            }

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.Id }, blogPost);
        }

        // PUT: api/BlogPosts/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPostDto blogPostDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRole = User.FindFirst(ClaimTypes.Role).Value;

            var blogPost = await _context.BlogPosts
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            // Users can only edit their own posts unless they're admin
            if (blogPost.AuthorId != userId && userRole != "Admin")
            {
                return Forbid();
            }

            blogPost.Title = blogPostDto.Title;
            blogPost.Content = blogPostDto.Content;
            blogPost.Excerpt = blogPostDto.Excerpt;
            blogPost.Slug = blogPostDto.Slug;
            blogPost.FeaturedImageUrl = blogPostDto.FeaturedImageUrl;
            blogPost.Status = blogPostDto.Status;
            blogPost.MetaTitle = blogPostDto.MetaTitle;
            blogPost.MetaDescription = blogPostDto.MetaDescription;
            blogPost.Tags = blogPostDto.Tags;
            blogPost.UpdatedAt = DateTime.UtcNow;

            if (blogPostDto.Status == "Published" && blogPost.PublishedAt == null)
            {
                blogPost.PublishedAt = DateTime.UtcNow;
            }

            // Update categories
            if (blogPostDto.CategoryIds.Any())
            {
                var categories = await _context.Categories
                    .Where(c => blogPostDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                blogPost.Categories = categories;
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }






        //changed code

        // DELETE: api/BlogPosts/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRole = User.FindFirst(ClaimTypes.Role).Value;

            // Include categories to handle the many-to-many relationship
            var blogPost = await _context.BlogPosts
                .Include(p => p.Categories) // This is crucial!
                .FirstOrDefaultAsync(p => p.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            // Users can only delete their own posts unless they're admin
            if (blogPost.AuthorId != userId && userRole != "Admin")
            {
                return Forbid();
            }

            // Remove category associations first (this clears the many-to-many relationship)
            blogPost.Categories.Clear();

            // Now remove the blog post
            _context.BlogPosts.Remove(blogPost);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting blog post with ID {BlogPostId}", id);
                return StatusCode(500, "Cannot delete post due to database constraints.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting blog post with ID {BlogPostId}", id);
                return StatusCode(500, "An unexpected error occurred.");
            }

            return NoContent();
        }



        // GET: api/BlogPosts/admin/pending (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/pending")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetPendingBlogPosts()
        {
            return await _context.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Categories)
                .Where(p => p.Status == "Pending")
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }

        // PUT: api/BlogPosts/admin/approve/5 (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/approve/{id}")]
        public async Task<IActionResult> ApproveBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Status = "Published";
            blogPost.PublishedAt = DateTime.UtcNow;
            blogPost.UpdatedAt = DateTime.UtcNow;

            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/BlogPosts/admin/reject/5 (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/reject/{id}")]
        public async Task<IActionResult> RejectBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Status = "Rejected";
            blogPost.UpdatedAt = DateTime.UtcNow;

            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}


