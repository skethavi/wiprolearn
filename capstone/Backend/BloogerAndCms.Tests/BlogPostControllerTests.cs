using BlogCMS.Controllers;
using BloggerAndCms.Controllers;
using BloggerAndCms.Data;
using BloggerAndCms.DTOs;
using BloggerAndCms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BloggerAndCms.Tests
{
    [TestFixture]
    public class BlogPostsControllerTests
    {
        private AppDbContext _context;
        private BlogPostsController _controller;
        private ILogger<BlogPostsController> _logger;

        [SetUp]
        public void Setup()
        {
            // Setup InMemory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogCmsTestDb")
                .Options;
            _context = new AppDbContext(options);

            // Seed a user
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                Role = "User"
            };
            _context.Users.Add(user);

            // Seed a category
            var category = new Category { Id = 1, Name = "Tech", Slug = "tech" };
            _context.Categories.Add(category);

            // Seed a blog post
            var blogPost = new BlogPost
            {
                Id = 1,
                Title = "First Post",
                Content = "Hello World",
                Excerpt = "Hello World Excerpt",
                Slug = "first-post",
                FeaturedImageUrl = "https://example.com/image.jpg",
                MetaTitle = "Meta title",
                MetaDescription = "Meta description",
                Tags = "intro,welcome",
                Status = "Published",
                AuthorId = user.Id,
                Author = user,
                Categories = new List<Category> { category }
            };
            _context.BlogPosts.Add(blogPost);

            _context.SaveChanges();

            // Setup logger
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<BlogPostsController>();

            // Setup controller
            _controller = new BlogPostsController(_context, _logger);

            // Mock authenticated user
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "User")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetBlogPosts_ShouldReturnOk_WithPosts()
        {
            var result = await _controller.GetBlogPosts();

            // Ensure the action returned OkObjectResult
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            // Extract the actual posts list
            var postsList = okResult.Value as IEnumerable<object>;
            Assert.That(postsList, Is.Not.Null);
            Assert.That(postsList.Any(), Is.True); // Check it's not empty
        }


        [Test]
        public async Task GetBlogPost_ShouldReturnOk_WhenPostExists()
        {
            var result = await _controller.GetBlogPost(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetBlogPost_ShouldReturnNotFound_WhenPostDoesNotExist()
        {
            var result = await _controller.GetBlogPost(999);

            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task PostBlogPost_ShouldReturnCreated_WithPost()
        {
            var newPost = new BlogPostDto
            {
                Title = "Second Post",
                Content = "Content for second post",
                Excerpt = "Excerpt",
                Slug = "second-post",
                FeaturedImageUrl = "https://example.com/second.jpg",
                MetaTitle = "Meta title 2",
                MetaDescription = "Meta description 2",
                Tags = "tech,news",
                Status = "Published",
                CategoryIds = new List<int> { 1 }
            };

            var result = await _controller.PostBlogPost(newPost);

            Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());

            var createdPost = (result.Result as CreatedAtActionResult).Value as BlogPost;
            Assert.That(createdPost, Is.Not.Null);
            Assert.That(createdPost.Title, Is.EqualTo("Second Post"));
        }

        [Test]
        public async Task PutBlogPost_ShouldUpdatePost()
        {
            var updateDto = new BlogPostDto
            {
                Title = "Updated First Post",
                Content = "Updated content",
                Excerpt = "Updated excerpt",
                Slug = "first-post-updated",
                FeaturedImageUrl = "https://example.com/image-updated.jpg",
                MetaTitle = "Updated Meta Title",
                MetaDescription = "Updated Meta Description",
                Tags = "update,test",
                Status = "Published",
                CategoryIds = new List<int> { 1 }
            };

            var result = await _controller.PutBlogPost(1, updateDto);
            Assert.That(result, Is.TypeOf<NoContentResult>());

            var updatedPost = await _context.BlogPosts.FindAsync(1);
            Assert.That(updatedPost.Title, Is.EqualTo("Updated First Post"));
        }

        [Test]
        public async Task DeleteBlogPost_ShouldRemovePost()
        {
            var result = await _controller.DeleteBlogPost(1);
            Assert.That(result, Is.TypeOf<NoContentResult>());

            var deletedPost = await _context.BlogPosts.FindAsync(1);
            Assert.That(deletedPost, Is.Null);
        }
    }
}
