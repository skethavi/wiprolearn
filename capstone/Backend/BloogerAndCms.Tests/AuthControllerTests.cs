using BloggerAndCms.Controllers;
using BloggerAndCms.Data;
using BloggerAndCms.DTOs;
using BloggerAndCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggerAndCms.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AppDbContext _context;
        private AuthController _controller;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            // In-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "AuthTestDb")
                .Options;

            _context = new AppDbContext(options);

            // Setup configuration for JWT
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "YourSuperSecretKeyHereMakeItLongEnoughForHMACSHA512AlgorithmAtLeast64CharactersLong!"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Setup controller
            _controller = new AuthController(_context, _configuration);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region Register Tests
        [Test]
        public async Task Register_ShouldReturnOk_WhenUserIsNew()
        {
            var registerDto = new RegisterDto
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "password123"
            };

            var result = await _controller.Register(registerDto) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value.ToString(), Does.Contain("User registered successfully"));
        }

        [Test]
        public async Task Register_ShouldReturnBadRequest_WhenUsernameExists()
        {
            // Seed user
            _context.Users.Add(new User { Username = "existinguser", Email = "other@example.com", PasswordHash = "hashed" });
            _context.SaveChanges();

            var registerDto = new RegisterDto
            {
                Username = "existinguser",
                Email = "new@example.com",
                Password = "password123"
            };

            var result = await _controller.Register(registerDto) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Username already exists"));
        }

        [Test]
        public async Task Register_ShouldReturnBadRequest_WhenEmailExists()
        {
            // Seed user
            _context.Users.Add(new User { Username = "user1", Email = "test@example.com", PasswordHash = "hashed" });
            _context.SaveChanges();

            var registerDto = new RegisterDto
            {
                Username = "newuser",
                Email = "test@example.com",
                Password = "password123"
            };

            var result = await _controller.Register(registerDto) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Email already exists"));
        }
        #endregion

        #region Login Tests

        [Test]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange: Seed user
            var user = new User { Username = "loginuser", Email = "login@example.com" };
            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "password123");
            _context.Users.Add(user);
            _context.SaveChanges();

            var loginDto = new LoginDto
            {
                Username = "loginuser",
                Password = "password123"
            };

            // Act
            var result = await _controller.Login(loginDto) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            // Convert anonymous object to dictionary
            var json = System.Text.Json.JsonSerializer.Serialize(result.Value);
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            Assert.That(dict, Is.Not.Null);
            Assert.That(dict.ContainsKey("token"), Is.True);
            Assert.That(dict.ContainsKey("user"), Is.True);

            var token = dict["token"].ToString();
            Assert.That(token, Is.Not.Null.And.Not.Empty);

            // Deserialize nested user object
            var userDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(dict["user"].ToString());
            Assert.That(userDict["Username"].ToString(), Is.EqualTo("loginuser"));
            Assert.That(userDict["Email"].ToString(), Is.EqualTo("login@example.com"));
        }

        

        [Test]
        public async Task Login_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            var loginDto = new LoginDto
            {
                Username = "nouser",
                Password = "pass123"
            };

            var result = await _controller.Login(loginDto) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("User not found"));
        }

        [Test]
        public async Task Login_ShouldReturnBadRequest_WhenPasswordIsWrong()
        {
            // Seed user
            var user = new User { Username = "loginuser", Email = "login@example.com" };
            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "correctpassword");
            _context.Users.Add(user);
            _context.SaveChanges();

            var loginDto = new LoginDto
            {
                Username = "loginuser",
                Password = "wrongpassword"
            };

            var result = await _controller.Login(loginDto) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Wrong password"));
        }
        #endregion
    }
}
