using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ALEHA_API.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private TransactionService _transactionService;
        private Mock<ITransactionDataProvider> _mockRepo;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository for testing
            _mockRepo = new Mock<ITransactionDataProvider>();
            _transactionService = new TransactionService(_mockRepo.Object);
        }

        [Test]
        public void Withdrawal_ValidTransactionRequest_ReturnsTransactionResponseModel()
        {
            // Arrange
            var transactionRequest = new TransactionRequestModel { /* Initialize with valid withdrawal request data */ };
            var transactionResponse = new TransactionResponseModel { /* Initialize with valid response data */ };
            _mockRepo.Setup(repo => repo.Withdrawal(It.IsAny<TransactionRequestModel>())).Returns(transactionResponse);

            // Act
            var result = _transactionService.Withdrawal(transactionRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactionResponse, result);
        }

        [Test]
        public void Deposit_ValidTransactionRequest_ReturnsTransactionResponseModel()
        {
            // Arrange
            var transactionRequest = new TransactionRequestModel { /* Initialize with valid deposit request data */ };
            var transactionResponse = new TransactionResponseModel { /* Initialize with valid response data */ };
            _mockRepo.Setup(repo => repo.Deposit(It.IsAny<TransactionRequestModel>())).Returns(transactionResponse);

            // Act
            var result = _transactionService.Deposit(transactionRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactionResponse, result);
        }

        [Test]
        public void Transfer_ValidTransferRequest_ReturnsTransactionResponseModel()
        {
            // Arrange
            var transferRequest = new TransactionRequestModel { /* Initialize with valid transfer request data */ };
            var transactionResponse = new TransactionResponseModel { /* Initialize with valid response data */ };
            _mockRepo.Setup(repo => repo.Transfer(It.IsAny<TransactionRequestModel>())).Returns(transactionResponse);

            // Act
            var result = _transactionService.Transfer(transferRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactionResponse, result);
        }

        [Test]
        public void Statement_ValidAccountNumber_ReturnsTransactionList()
        {
            // Arrange
            int accountNumber = 123; // Replace with a valid account number
            var transactions = new List<Transaction> { /* Initialize with valid transactions */ };
            _mockRepo.Setup(repo => repo.Statement(accountNumber)).Returns(transactions);

            // Act
            var result = _transactionService.Statement(accountNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions, result);
        }

        [Test]
        public void CurrencyExchange_ValidCurrency_ReturnsConvertedAmount()
        {
            // Arrange
            var currency = "USD"; // Replace with a valid currency code
            decimal value = 100.0m; // Replace with a valid amount
            var exchangeRatesList = new Dictionary<string, decimal>
            {
                { "USD", 1.0m },
                { "EUR", 0.85m },
                // Add more exchange rates as needed
            };
            var jsonString = System.Text.Json.JsonSerializer.Serialize(exchangeRatesList);

            // Mock reading exchange rates from a file
            _mockRepo.Setup(repo => repo.ReadAllText("./Repo/exchangeRates.json")).Returns(jsonString);

            // Act
            decimal result = _transactionService.CurrencyExchange(currency, value);

            // Assert
            Assert.AreEqual(value, result); // Since the currency is USD, it should return the same value
        }

        [Test]
        public void CurrencyExchange_FileReadError_ReturnsOriginalValue()
        {
            // Arrange
            var currency = "EUR"; // Replace with a valid currency code
            decimal value = 100.0m; // Replace with a valid amount

            // Simulate an IOException when reading exchange rates from a file
            _mockRepo.Setup(repo => repo.ReadAllText("./Repo/exchangeRates.json")).Throws(new IOException("Simulated error"));

            // Act
            decimal result = _transactionService.CurrencyExchange(currency, value);

            // Assert
            Assert.AreEqual(value, result); // It should return the original value due to the error
        }

        [Test]
public void Withdrawal_InvalidTransactionRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid withdrawal request data */ };

    // Act
    var result = _transactionService.Withdrawal(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid withdrawal request", result.ErrorMessage);
}

[Test]
public void Deposit_InvalidTransactionRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid deposit request data */ };

    // Act
    var result = _transactionService.Deposit(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid deposit request", result.ErrorMessage);
}

[Test]
public void Transfer_InvalidTransferRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid transfer request data */ };

    // Act
    var result = _transactionService.Transfer(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid transfer request", result.ErrorMessage);
}

[Test]
public void CurrencyExchange_InvalidCurrency_ReturnsOriginalValue()
{
    // Arrange
    var invalidCurrency = "INVALID"; // Replace with an invalid currency code
    decimal value = 100.0m; // Replace with a valid amount

    // Act
    decimal result = _transactionService.CurrencyExchange(invalidCurrency, value);

    // Assert
    Assert.AreEqual(value, result); // It should return the original value for an invalid currency
}

[Test]
public void CurrencyExchange_ExchangeRateNotFound_ReturnsOriginalValue()
{
    // Arrange
    var currency = "GBP"; // Replace with a currency code that's not in the exchange rates list
    decimal value = 100.0m; // Replace with a valid amount

    // Mock reading exchange rates from a file with a missing currency
    var jsonString = System.Text.Json.JsonSerializer.Serialize(new Dictionary<string, decimal>());
    _mockRepo.Setup(repo => repo.ReadAllText("./Repo/exchangeRates.json")).Returns(jsonString);

    // Act
    decimal result = _transactionService.CurrencyExchange(currency, value);

    // Assert
    Assert.AreEqual(value, result); // It should return the original value since the exchange rate is not found
}
[Test]
public void Withdrawal_InvalidTransactionRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid withdrawal request data */ };

    // Act
    var result = _transactionService.Withdrawal(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid withdrawal request", result.ErrorMessage);
}

[Test]
public void Deposit_InvalidTransactionRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid deposit request data */ };

    // Act
    var result = _transactionService.Deposit(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid deposit request", result.ErrorMessage);
}

[Test]
public void Transfer_InvalidTransferRequest_ReturnsErrorMessage()
{
    // Arrange
    var invalidRequest = new TransactionRequestModel { /* Initialize with invalid transfer request data */ };

    // Act
    var result = _transactionService.Transfer(invalidRequest);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Invalid transfer request", result.ErrorMessage);
}

[Test]
public void CurrencyExchange_InvalidCurrency_ReturnsOriginalValue()
{
    // Arrange
    var invalidCurrency = "INVALID"; // Replace with an invalid currency code
    decimal value = 100.0m; // Replace with a valid amount

    // Act
    decimal result = _transactionService.CurrencyExchange(invalidCurrency, value);

    // Assert
    Assert.AreEqual(value, result); // It should return the original value for an invalid currency
}

[Test]
public void CurrencyExchange_ExchangeRateNotFound_ReturnsOriginalValue()
{
    // Arrange
    var currency = "GBP"; // Replace with a currency code that's not in the exchange rates list
    decimal value = 100.0m; // Replace with a valid amount

    // Mock reading exchange rates from a file with a missing currency
    var jsonString = System.Text.Json.JsonSerializer.Serialize(new Dictionary<string, decimal>());
    _mockRepo.Setup(repo => repo.ReadAllText("./Repo/exchangeRates.json")).Returns(jsonString);

    // Act
    decimal result = _transactionService.CurrencyExchange(currency, value);

    // Assert
    Assert.AreEqual(value, result); // It should return the original value since the exchange rate is not found
}

    }
}
