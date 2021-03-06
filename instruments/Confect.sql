CREATE TABLE Users(
Id INT IDENTITY(1,1) PRIMARY KEY,
Role INT DEFAULT '0',
FirstName VARCHAR(10) NOT NULL,
LastName VARCHAR(15) NOT NULL,
Password VARCHAR(10) NOT NULL,
Phone VARCHAR(19) NOT NULL,
Gender VARCHAR(10) NOT NULL,
DOB DATETIME NOT NULL,
);

CREATE TABLE Device(
Id INT PRIMARY KEY IDENTITY(1,1),
DeviceId VARCHAR(20) NOT NULL,
UserId INT REFERENCES Users (Id) ON UPDATE CASCADE NULL,
);

CREATE TABLE Point(
Id INT PRIMARY KEY IDENTITY(1,1), 
UserId INT REFERENCES Users (Id) ON UPDATE CASCADE NOT NULL, 
Date DATETIME DEFAULT GETDATE(),
Point INT
);

CREATE TABLE Basket(
Id INT PRIMARY KEY IDENTITY(1,1),
DeviceId INT REFERENCES Device (Id) ON UPDATE CASCADE NOT NULL,
ProductId INT REFERENCES Products (Id) ON UPDATE CASCADE NOT NULL,
BasketAmount INT DEFAULT '1',
Price INT NOT NULL
);

CREATE TABLE Products(
Id INT PRIMARY KEY IDENTITY(1,1),
Category VARCHAR(10) NOT NULL,
Photo VARCHAR(25) NOT NULL,
Title NVARCHAR(20) NOT NULL,
Description NVARCHAR(MAX) NOT NULL,
ProductAmount NVARCHAR(50) NOT NULL, 
Price INT NOT NULL, 
);

CREATE TABLE Push(
Id INT IDENTITY(1,1) PRIMARY KEY,
Date DATETIME DEFAULT GETDATE() NOT NULL,
Header VARCHAR(35) NOT NULL,
Text VARCHAR(MAX) NOT NULL
);

CREATE TABLE Orders(
Id INT IDENTITY(1,1) PRIMARY KEY,
Date DATETIME DEFAULT GETDATE() NOT NULL,
Nomination VARCHAR(30) NOT NULL,
UserId INT REFERENCES Users (Id) ON UPDATE CASCADE NOT NULL,
Products VARCHAR(MAX),
Comment VARCHAR(MAX),
GPS VARCHAR(MAX),
Time VARCHAR(30),
Payment VARCHAR(30),
Total INT,
Status VARCHAR(30) DEFAULT '?????', 
);

CREATE TABLE Courier(
Id INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(10) NOT NULL,
Phone VARCHAR(19) NOT NULL,
Zone VARCHAR(MAX) NOT NULL
);

CREATE TABLE Reviews(
Id INT IDENTITY(1,1) PRIMARY KEY,
Date DATETIME DEFAULT GETDATE() NOT NULL,
Speed INT NOT NULL,
Kitchen INT NOT NULL,
Service INT NOT NULL,
Text VARCHAR(MAX) NOT NULL,
);
