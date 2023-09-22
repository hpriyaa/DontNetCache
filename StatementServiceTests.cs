using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ALEHA_API.Tests
{
    [TestFixture]
    public class StatementServiceTests
    {
        private StatementService _statementService;
        private Mock<ITransactionDataProvider> _mockRepo;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository for testing
            _mockRepo = new Mock<ITransactionDataProvider>();
            _statementService = new StatementService(_mockRepo.Object);
        }

        [Test]
        public void Statement_ValidAccountNumber_ReturnsTransactionList()
        {
            // Arrange
            int accountNumber = 123; // Replace with a valid account number
            var transactions = new List<Transaction> { /* Initialize with valid transactions */ };
            _mockRepo.Setup(repo => repo.Statement(accountNumber)).Returns(transactions);

            // Act
            var result = _statementService.Statement(accountNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions, result);
        }

        [Test]
        public void Statement_InvalidAccountNumber_ReturnsEmptyList()
        {
            // Arrange
            int invalidAccountNumber = -1; // Replace with an invalid account number
            _mockRepo.Setup(repo => repo.Statement(invalidAccountNumber)).Returns(new List<Transaction>());

            // Act
            var result = _statementService.Statement(invalidAccountNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
public void Statement_EmptyStatements_ReturnsEmptyList()
{
    // Arrange
    int accountNumber = 123; // Replace with a valid account number
    _mockRepo.Setup(repo => repo.Statement(accountNumber)).Returns(new List<Transaction>()); // Simulate empty statements

    // Act
    var result = _statementService.Statement(accountNumber);

    // Assert
    Assert.IsNotNull(result);
    Assert.IsEmpty(result);
}

[Test]
public void Statement_RepositoryThrowsException_ReturnsEmptyList()
{
    // Arrange
    int accountNumber = 123; // Replace with a valid account number
    _mockRepo.Setup(repo => repo.Statement(accountNumber)).Throws(new Exception("Simulated exception"));

    // Act
    var result = _statementService.Statement(accountNumber);

    // Assert
    Assert.IsNotNull(result);
    Assert.IsEmpty(result);
}

[Test]
public void Statement_InvalidAccountNumber_ReturnsEmptyList()
{
    // Arrange
    int invalidAccountNumber = -1; // Replace with an invalid account number
    _mockRepo.Setup(repo => repo.Statement(invalidAccountNumber)).Returns(new List<Transaction>());

    // Act
    var result = _statementService.Statement(invalidAccountNumber);

    // Assert
    Assert.IsNotNull(result);
    Assert.IsEmpty(result);
}

    }
}
