using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Moq;
using NUnit.Framework;

namespace ALEHA_API.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private Mock<ICustomerDataProvider<Customer>> _mockRepo;

        [SetUp]
        public void Setup()
        {
            // Create a mock repository for testing
            _mockRepo = new Mock<ICustomerDataProvider<Customer>>();
            _customerService = new CustomerService(_mockRepo.Object);
        }

        [Test]
        public void AddCustomerDetails_ValidCustomer_ReturnsCustomerId()
        {
            // Arrange
            var customer = new Customer { /* Initialize with valid customer data */ };
            _mockRepo.Setup(repo => repo.AddCustomerDetails(It.IsAny<Customer>())).Returns(123); // Simulate a successful addition

            // Act
            string result = _customerService.AddCustomerDetails(customer);

            // Assert
            Assert.AreEqual("123", result); // Check if it returns the customer ID as a string
        }

        [Test]
        public void AddCustomerDetails_FailedToAddCustomer_ReturnsErrorMessage()
        {
            // Arrange
            var customer = new Customer { /* Initialize with valid customer data */ };
            _mockRepo.Setup(repo => repo.AddCustomerDetails(It.IsAny<Customer>())).Returns(0); // Simulate failure to add

            // Act
            string result = _customerService.AddCustomerDetails(customer);

            // Assert
            Assert.AreEqual("Failed to add Customer", result);
        }

        [Test]
        public void ViewAccounts_ValidCustomerId_ReturnsViewAccountsResponse()
        {
            // Arrange
            int customerId = 123; // Replace with a valid customer ID
            var viewAccountsResponse = new ViewAccountsResponse { /* Initialize with valid response data */ };
            _mockRepo.Setup(repo => repo.ViewAccounts(customerId)).Returns(viewAccountsResponse);

            // Act
            var result = _customerService.ViewAccounts(customerId);

            // Assert
            Assert.AreEqual(viewAccountsResponse, result);
        }

        [Test]
        public void AddCustomerDetails_InvalidCustomerData_ReturnsErrorMessage()
        {
            // Arrange
            var invalidCustomer = new Customer { /* Initialize with invalid customer data */ };
        
            // Act
            string result = _customerService.AddCustomerDetails(invalidCustomer);
        
            // Assert
            Assert.AreEqual("Failed to add Customer", result);
        }
        
        [Test]
        public void ViewAccounts_CustomerNotFound_ReturnsNull()
        {
            // Arrange
            int customerId = 456; // Replace with a customer ID that does not exist
            _mockRepo.Setup(repo => repo.ViewAccounts(customerId)).Returns((ViewAccountsResponse)null); // Simulate customer not found
        
            // Act
            var result = _customerService.ViewAccounts(customerId);
        
            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public void ViewAccounts_FailedToRetrieveAccounts{
        {
            // Arrange
            int customerId = 123; // Replace with a valid customer ID
            _mockRepo.Setup(repo => repo.ViewAccounts(customerId)).Returns((ViewAccountsResponse)null); // Simulate failure to retrieve accounts
        
            // Act
            var result = _customerService.ViewAccounts(customerId);
        
            // Assert
            Assert.IsNull(result);
        }

    }
}
