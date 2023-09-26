using System;
using System.Collections.Generic;
using ALEHA_API.Models;
using ALEHA_API.Repository;
using ALEHA_API.Services;
using Moq;
using NUnit.Framework;

[TestFixture]
public class CustomerServiceTests
{
    private ICustomerDataProvider<Customer> _customerDataProvider;
    private CustomerService _customerService;

    [SetUp]
    public void Setup()
    {
        // Create a mock for the customer data provider
        var customerDataProviderMock = new Mock<ICustomerDataProvider<Customer>>();

        // Set up the mock's behavior for AddCustomerDetails
        customerDataProviderMock.Setup(repo => repo.AddCustomerDetails(It.IsAny<Customer>()))
            .Returns((Customer customer) =>
            {
                // Simulate adding the customer and returning a customer ID
                if (customer.CustomerName != null)
                {
                    return 1; // Return a non-zero customer ID for success
                }
                return 0; // Return 0 for failure
            });

        // Set up the mock's behavior for ViewAccounts
        customerDataProviderMock.Setup(repo => repo.ViewAccounts(It.IsAny<int>()))
            .Returns((int customerId) =>
            {
                // Simulate returning a list of accounts for a customer
                if (customerId == 1)
                {
                    return new List<ViewAccountsResponse>
                    {
                        new ViewAccountsResponse
                        {
                            AccountType = "Savings",
                            AccountNumber = 101
                        },
                        new ViewAccountsResponse
                        {
                            AccountType = "Checking",
                            AccountNumber = 102
                        }
                    };
                }
                return new List<ViewAccountsResponse>(); // Return an empty list for other cases
            });

        // Set up the mock's behavior for GetCustomerId
        customerDataProviderMock.Setup(repo => repo.GetCustomerId(It.IsAny<string>()))
            .Returns((string userEmail) =>
            {
                // Simulate returning a customer ID based on the user's email
                if (userEmail == "test@example.com")
                {
                    return 1;
                }
                return 0; // Return 0 for unknown users
            });

        // Set up the mock's behavior for GetCustomers
        customerDataProviderMock.Setup(repo => repo.GetCustomers())
            .Returns(new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Test User" },
                new Customer { CustomerId = 2, CustomerName = "Another User" }
            });

        // Set up the mock's behavior for GetCustomerName
        customerDataProviderMock.Setup(repo => repo.GetCustomerName(It.IsAny<string>()))
            .Returns((string userEmail) =>
            {
                // Simulate returning a customer's name based on the user's email
                if (userEmail == "test@example.com")
                {
                    return "Test User";
                }
                return null; // Return null for unknown users
            });

        // Set up the mock's behavior for ValidateAccount
        customerDataProviderMock.Setup(repo => repo.ValidateAccount(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((int customerId, int accountNumber) =>
            {
                // Simulate account validation based on customer ID and account number
                if (customerId == 1 && (accountNumber == 101 || accountNumber == 102))
                {
                    return true; // Return true for valid accounts
                }
                return false; // Return false for invalid accounts
            });

        // Set up the mock's behavior for EditCustomer
        customerDataProviderMock.Setup(repo => repo.EditCustomer(It.IsAny<Customer>()))
            .Returns((Customer customer) =>
            {
                // Simulate editing a customer and returning a status message
                if (customer.CustomerName != null)
                {
                    return "SUCCESS"; // Return "SUCCESS" for success
                }
                return "FAILED"; // Return "FAILED" for failure
            });

        _customerDataProvider = customerDataProviderMock.Object;
        _customerService = new CustomerService(_customerDataProvider);
    }

    [Test]
    public void AddCustomerDetails_ValidCustomer_ReturnsCustomerId()
    {
        // Arrange
        var customer = new Customer
        {
            CustomerName = "Test User",
            Dob = DateTime.Now,
            PhoneNumber = "1234567890",
            Email = "test@example.com"
        };

        // Act
        var result = _customerService.AddCustomerDetails(customer);

        // Assert
        Assert.AreEqual("1", result); // The mocked data provider always returns 1
    }

    [Test]
    public void AddCustomerDetails_NullCustomer_ReturnsFailed()
    {
        // Act
        var result = _customerService.AddCustomerDetails(null);

        // Assert
        Assert.AreEqual("Failed to add Customer", result);
    }

    [Test]
public void ViewAccounts_ValidCustomerId_ReturnsAccountList()
{
    // Arrange
    var customerId = 1;

    // Act
    var result = _customerService.ViewAccounts(customerId);

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<List<ViewAccountsResponse>>(result);
    Assert.AreEqual(2, result.Count); // Expecting 2 accounts for the mocked customer ID
}

[Test]
public void ViewAccounts_InvalidCustomerId_ReturnsEmptyList()
{
    // Arrange
    var customerId = 999; // Non-existent customer ID

    // Act
    var result = _customerService.ViewAccounts(customerId);

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<List<ViewAccountsResponse>>(result);
    Assert.IsEmpty(result); // Expecting an empty list for an invalid customer ID
}

[Test]
public void GetCustomerId_ValidUserEmail_ReturnsCustomerId()
{
    // Arrange
    var userEmail = "test@example.com";

    // Act
    var result = _customerService.GetCustomerId(userEmail);

    // Assert
    Assert.AreEqual(1, result); // Expecting customer ID 1 for the mocked user email
}

[Test]
public void GetCustomerId_InvalidUserEmail_ReturnsZero()
{
    // Arrange
    var userEmail = "invalid@example.com"; // Non-existent user email

    // Act
    var result = _customerService.GetCustomerId(userEmail);

    // Assert
    Assert.AreEqual(0, result); // Expecting 0 for an invalid user email
}

[Test]
public void GetCustomers_ReturnsCustomerList()
{
    // Act
    var result = _customerService.GetCustomers();

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<List<Customer>>(result);
    Assert.AreEqual(2, result.Count); // Expecting 2 customers for the mocked data provider
}

[Test]
public void GetCustomerName_ValidUserEmail_ReturnsCustomerName()
{
    // Arrange
    var userEmail = "test@example.com";

    // Act
    var result = _customerService.GetCustomerName(userEmail);

    // Assert
    Assert.AreEqual("Test User", result); // Expecting customer name for the mocked user email
}

[Test]
public void GetCustomerName_InvalidUserEmail_ReturnsNull()
{
    // Arrange
    var userEmail = "invalid@example.com"; // Non-existent user email

    // Act
    var result = _customerService.GetCustomerName(userEmail);

    // Assert
    Assert.IsNull(result); // Expecting null for an invalid user email
}

[Test]
public void ValidateAccount_ValidAccount_ReturnsTrue()
{
    // Arrange
    var currentUser = "test@example.com";
    var accountNumber = 101; // Valid account number for the mocked customer

    // Act
    var result = _customerService.ValidateAccount(currentUser, accountNumber);

    // Assert
    Assert.IsTrue(result); // Expecting true for a valid account
}

[Test]
public void ValidateAccount_InvalidAccount_ReturnsFalse()
{
    // Arrange
    var currentUser = "test@example.com";
    var accountNumber = 999; // Invalid account number

    // Act
    var result = _customerService.ValidateAccount(currentUser, accountNumber);

    // Assert
    Assert.IsFalse(result); // Expecting false for an invalid account
}

[Test]
public void EditCustomer_ValidCustomer_ReturnsSuccess()
{
    // Arrange
    var customer = new Customer
    {
        CustomerName = "Updated User",
        PhoneNumber = "9876543210",
        Email = "test@example.com",
        CustomerAddress = "Updated Address",
        City = "Updated City"
    };

    // Act
    var result = _customerService.EditCustomer(customer);

    // Assert
    Assert.AreEqual("SUCCESS", result); // Expecting "SUCCESS" for a valid edit
}

[Test]
public void EditCustomer_NullCustomer_ReturnsFailed()
{
    // Act
    var result = _customerService.EditCustomer(null);

    // Assert
    Assert.AreEqual("FAILED", result); // Expecting "FAILED" for a null customer
}

[Test]
public void EditCustomer_CustomerNotFound_ReturnsFailed()
{
    // Arrange
    var customer = new Customer
    {
        CustomerName = "Updated User",
        PhoneNumber = "9876543210",
        Email = "invalid@example.com", // Non-existent email
        CustomerAddress = "Updated Address",
        City = "Updated City"
    };

    // Act
    var result = _customerService.EditCustomer(customer);

    // Assert
    Assert.AreEqual("FAILED", result); // Expecting "FAILED" for a customer not found
}

}

}
