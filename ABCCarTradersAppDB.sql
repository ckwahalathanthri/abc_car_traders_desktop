CREATE DATABASE ABCCarTradersApp;
USE ABCCarTradersApp;

-- Users Table
CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Phone VARCHAR(20),
    Address TEXT,
    UserType ENUM('Admin', 'Customer') NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Categories Table
CREATE TABLE Categories (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL,
    CategoryType ENUM('Car', 'Part') NOT NULL
);

-- Cars Table
CREATE TABLE Cars (
    CarID INT AUTO_INCREMENT PRIMARY KEY,
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Year INT NOT NULL,
    Price DECIMAL(12,2) NOT NULL,
    Color VARCHAR(30),
    Mileage INT,
    FuelType VARCHAR(20),
    Transmission VARCHAR(20),
    Description TEXT,
    ImagePath VARCHAR(255),
    CategoryID INT,
    IsAvailable BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Car Parts Table
CREATE TABLE CarParts (
    PartID INT AUTO_INCREMENT PRIMARY KEY,
    PartName VARCHAR(100) NOT NULL,
    PartNumber VARCHAR(50) UNIQUE,
    Brand VARCHAR(50),
    Price DECIMAL(10,2) NOT NULL,
    Description TEXT,
    ImagePath VARCHAR(255),
    CategoryID INT,
    StockQuantity INT DEFAULT 0,
    IsAvailable BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Orders Table
CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(12,2) NOT NULL,
    Status ENUM('Pending', 'Processing', 'Completed', 'Cancelled') DEFAULT 'Pending',
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
);

-- Order Items Table
CREATE TABLE OrderItems (
    OrderItemID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    ItemType ENUM('Car', 'Part') NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT DEFAULT 1,
    Price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- Insert sample data
INSERT INTO Categories (CategoryName, CategoryType) VALUES
('Sedan', 'Car'), ('SUV', 'Car'), ('Hatchback', 'Car'),
('Engine Parts', 'Part'), ('Brake Parts', 'Part'), ('Body Parts', 'Part');

INSERT INTO Users (Username, Password, FullName, Email, UserType) VALUES
('admin', 'admin123', 'System Administrator', 'admin@abccartraders.com', 'Admin');
*/