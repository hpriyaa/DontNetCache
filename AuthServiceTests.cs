using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ALEHA_API.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private Mock<IAuthDataProvider<Auth>> _mockRepo;
        private Mock<IConfiguration> _mockConfig;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository and mock configuration for testing
            _mockRepo = new Mock<IAuthDataProvider<Auth>>();
            _mockConfig = new Mock<IConfiguration>();

            // Set up configuration values required for JWT
            _mockConfig.SetupGet(x => x["Jwt:Key"]).Returns("your-secret-key");
            _mockConfig.SetupGet(x => x["Jwt:Issuer"]).Returns("your-issuer");

            _authService = new AuthService(_mockRepo.Object, _mockConfig.Object);
        }

        [Test]
        public void GenerateJSONWebToken_ValidAuth_ReturnsToken()
        {
            // Arrange
            var userInfo = new Auth { UserEmail = "testuser", UserRole = "user-role" };

            // Act
            var token = _authService.GenerateJSONWebToken(userInfo);

            // Assert
            Assert.IsNotNull(token);
        }

        [Test]
        public void GenerateJSONWebToken_NullUserInfo_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => _authService.GenerateJSONWebToken(null));
        }

        [Test]
        public void AuthenticateUser_ValidCredentials_ReturnsAuth()
        {
            // Arrange
            var login = new Auth { UserEmail = "testuser", UserPassword = "testpassword" };
            _mockRepo.Setup(repo => repo.ValidateAuthDetails(It.IsAny<Auth>())).Returns(login);

            // Act
            var result = _authService.AuthenticateUser(login);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterUser_ValidRegistration_ReturnsAuth()
        {
            // Arrange
            var register = new Auth { UserEmail = "newuser", UserPassword = "newpassword", UserRole = "user-role" };
            _mockRepo.Setup(repo => repo.RegisterAuth(It.IsAny<Auth>())).Returns(register);

            // Act
            var result = _authService.RegisterUser(register);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AuthenticateUser_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var invalidLogin = new Auth { UserEmail = "invaliduser", UserPassword = "invalidpassword" };
            _mockRepo.Setup(repo => repo.ValidateAuthDetails(It.IsAny<Auth>())).Returns((Auth)null); // Simulate invalid credentials
        
            // Act
            var result = _authService.AuthenticateUser(invalidLogin);
        
            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public void RegisterUser_InvalidRegistration_ReturnsNull()
        {
            // Arrange
            var invalidRegister = new Auth { UserEmail = "existinguser", UserPassword = "newpassword", UserRole = "user-role" };
            _mockRepo.Setup(repo => repo.RegisterAuth(It.IsAny<Auth>())).Returns((Auth)null); // Simulate registration failure
        
            // Act
            var result = _authService.RegisterUser(invalidRegister);
        
            // Assert
            Assert.IsNull(result);
        }

    }
}
