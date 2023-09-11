CREATE DATABASE ALEHABankDB

use ALEHABankDB

--Creating the Authentication Table
CREATE TABLE Auth(
	user_email NVARCHAR(40) PRIMARY KEY,
	user_password VARCHAR(16) NOT NULL CONSTRAINT CHK_pwdLength CHECK (LEN(user_password) = 16) ,
	user_role VARCHAR(10) NOT NULL CONSTRAINT CHK_role CHECK (user_role IN('ADMIN','CUSTOMER'))
)

--Inserting Values into Authentication Table
INSERT INTO Auth Values
('rammey@gmail.com','qwertyuikjhgfdsa','ADMIN')
 
--Creating the Customer TABLE
CREATE TABLE Customer
(
	customer_id INT PRIMARY KEY identity(500,1),
	customer_name VARCHAR(20) NOT NULL,
	dob DATE NOT NULL,
	phone_number CHAR(10) UNIQUE CONSTRAINT CHK_phone_number
	CHECK (phone_number LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	email VARCHAR(30) FOREIGN KEY REFERENCES Auth(user_email),
	customer_address VARCHAR(30),
	city VARCHAR(20),
	aadhaar_number CHAR(12) CONSTRAINT CHK_aadhaar_number
	CHECK (aadhaar_number LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') UNIQUE NOT NULL
)

--Inserting Values Into the Customer Table
INSERT INTO Customer VALUES 
('Ram','2001-07-24','9191919191','ram@gmail.com','Embassy Spl Zone','Chennai', '919191919191'),
('Rani','2001-09-24','9191919192','rani@gmail.com','Velachery','Chennai', '919191919192'),
('Raju','2001-02-24','9191919193','raju@gmail.com','Pallavaram','Chennai', '919191919193'),
('Anshul','2001-10-24','9191919194','anshul@gmail.com','Toraipakkam','Chennai', '919191919194'),
('Apoorv','2001-12-24','9191919195','apoorv@gmail.com','Anna Nagar','Chennai', '919191919195')


--Creating the Account Table
CREATE TABLE Account
(
	account_number INT PRIMARY KEY identity(100,1),
	customer_id INT FOREIGN KEY REFERENCES Customer(customer_id),
	balance float NOT NULL,
	account_type VARCHAR(40) NOT NULL CONSTRAINT CHK_accountType CHECK (account_type IN('Salary','Savings','Current')) DEFAULT 'Savings',
	card_number VARCHAR(16) UNIQUE CONSTRAINT CHK_length
	CHECK (card_number LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL,
	card_pin VARCHAR(4) UNIQUE CONSTRAINT CHK_pinLength
	CHECK (card_pin LIKE '[0-9][0-9][0-9][0-9]') NOT NULL,
	DATE_of_creation DATE DEFAULT GETUTCDATE()
 )
  
--Inserting Values Into the Accounts Table
INSERT Into Account VALUES
('501', 420,'Savings','1234567812345678','6969', '2018-07-11'),
('500', 1729,'Current','9988776655443322','1234', '2016-01-20')

--Creating the Transactions Table
CREATE	TABLE Transactions(
	transaction_number INT PRIMARY KEY IDENTITY(10,1),
	account_number INT REFERENCES Account(account_number),
	transaction_type VARCHAR(20) NOT NULL CONSTRAINT CHK_transactionType CHECK (transaction_type IN('WITHDRAWAL','DEPOSIT')),
	transaction_amount FLOAT(24) NOT NULL,
	transaction_timestamp DATE DEFAULT GETUTCDATE()	
)

--Inserting Values Into the Transactions Table
INSERT Into Transactions VALUES
(100,'WITHDRAWAL',20.0,'2023-09-08 11:10:20.687'),
(101,'DEPOSIT',40.0,'2023-09-08 11:15:20.687')

