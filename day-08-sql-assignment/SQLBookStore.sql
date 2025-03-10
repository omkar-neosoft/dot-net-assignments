-- Create the database if not exists
IF NOT EXISTS (SELECT NAME
               FROM   sys.databases
               WHERE  NAME = '	')
  BEGIN
      CREATE DATABASE BookStoreDB;
  END

go

USE BookStoreDB;

-- Create Authors table if not exists
IF NOT EXISTS (SELECT *
               FROM   sysobjects
               WHERE  NAME = 'Authors'
                      AND xtype = 'U')
  BEGIN
      CREATE TABLE authors
        (
           authorid INT PRIMARY KEY IDENTITY(1, 1),
           NAME     NVARCHAR(255) NOT NULL,
           country  NVARCHAR(100)
        );
  END

-- Create Books table if not exists
IF NOT EXISTS (SELECT *
               FROM   sysobjects
               WHERE  NAME = 'Books'
                      AND xtype = 'U')
  BEGIN
      CREATE TABLE books
        (
           bookid        INT PRIMARY KEY IDENTITY(1, 1),
           title         NVARCHAR(255) NOT NULL UNIQUE,
           authorid      INT,
           price         DECIMAL(10, 2) NOT NULL,
           publishedyear INT,
           FOREIGN KEY (authorid) REFERENCES authors(authorid) ON DELETE CASCADE
        );
  END

-- Create Customers table if not exists
IF NOT EXISTS (SELECT *
               FROM   sysobjects
               WHERE  NAME = 'Customers'
                      AND xtype = 'U')
  BEGIN
      CREATE TABLE customers
        (
           customerid  INT PRIMARY KEY IDENTITY(1, 1),
           NAME        NVARCHAR(255) NOT NULL,
           email       NVARCHAR(255) UNIQUE NOT NULL,
           phonenumber NVARCHAR(15) UNIQUE
        );
  END

-- Create Orders table if not exists
IF NOT EXISTS (SELECT *
               FROM   sysobjects
               WHERE  NAME = 'Orders'
                      AND xtype = 'U')
  BEGIN
      CREATE TABLE orders
        (
           orderid     INT PRIMARY KEY IDENTITY(1, 1),
           customerid  INT,
           orderdate   DATE NOT NULL,
           totalamount DECIMAL(10, 2) NOT NULL,
           FOREIGN KEY (customerid) REFERENCES customers(customerid) ON DELETE
           CASCADE
        );
  END

-- Create OrderItems table if not exists
IF NOT EXISTS (SELECT *
               FROM   sysobjects
               WHERE  NAME = 'OrderItems'
                      AND xtype = 'U')
  BEGIN
      CREATE TABLE orderitems
        (
           orderitemid INT PRIMARY KEY IDENTITY(1, 1),
           orderid     INT,
           bookid      INT,
           quantity    INT NOT NULL,
           subtotal    DECIMAL(10, 2) NOT NULL,
           FOREIGN KEY (orderid) REFERENCES orders(orderid) ON DELETE CASCADE,
           FOREIGN KEY (bookid) REFERENCES books(bookid) ON DELETE CASCADE
        );
  END

-- Insert data into Authors
INSERT INTO authors
            (NAME,
             country)
VALUES      ('Chetan Bhagat',
             'India'),
            ('R.K. Narayan',
             'India'),
            ('Amish Tripathi',
             'India'),
            ('Ruskin Bond',
             'India'),
            ('Arundhati Roy',
             'India');

-- Insert data into Books
INSERT INTO books
            (title,
             authorid,
             price,
             publishedyear)
VALUES      ('SQL Mastery',
             1,
             2500,
             2020),
            ('Malgudi Days',
             2,
             1500,
             1943),
            ('The Immortals of Meluha',
             3,
             1800,
             2010),
            ('The Blue Umbrella',
             4,
             2200,
             1980),
            ('The God of Small Things',
             5,
             2600,
             1997);

-- Insert data into Customers
INSERT INTO customers
            (NAME,
             email,
             phonenumber)
VALUES      ('Amit Sharma',
             'amit@example.com',
             '9876543210'),
            ('Rahul Verma',
             'rahul@example.com',
             '8765432109'),
            ('Priya Singh',
             'priya@example.com',
             '7654321098'),
            ('Neha Kapoor',
             'neha@example.com',
             '6543210987'),
            ('Vikram Joshi',
             'vikram@example.com',
             '5432109876');

-- Insert data into Orders
INSERT INTO orders
            (customerid,
             orderdate,
             totalamount)
VALUES      (1,
             '2024-03-01',
             5000),
            (2,
             '2024-02-15',
             2200),
            (3,
             '2024-02-20',
             1800);

-- Insert data into OrderItems
INSERT INTO orderitems
            (orderid,
             bookid,
             quantity,
             subtotal)
VALUES      (1,
             1,
             2,
             5000),
            (2,
             4,
             1,
             2200),
            (3,
             3,
             1,
             1800);

-- Update book price
UPDATE books
SET    price = price * 1.10
WHERE  title = 'SQL Mastery';

-- Delete customers without orders
DELETE FROM customers
WHERE  customerid NOT IN (SELECT DISTINCT customerid
                          FROM   orders);

-- Queries using Operators
SELECT *
FROM   books
WHERE  price > 2000;

SELECT Count(*) AS TotalBooks
FROM   books;

SELECT *
FROM   books
WHERE  publishedyear BETWEEN 2015 AND 2023;

SELECT DISTINCT customers.*
FROM   customers
       JOIN orders
         ON customers.customerid = orders.customerid;

SELECT *
FROM   books
WHERE  title LIKE '%SQL%';

SELECT TOP 1 *
FROM   books
ORDER  BY price DESC;

SELECT *
FROM   customers
WHERE  NAME LIKE 'A%'
        OR NAME LIKE 'J%';

SELECT Sum(totalamount) AS TotalRevenue
FROM   orders;

-- Queries using Joins
SELECT books.title,
       authors.NAME
FROM   books
       JOIN authors
         ON books.authorid = authors.authorid;

SELECT customers.customerid,
       customers.NAME,
       Count(orders.orderid) AS TotalOrders
FROM   customers
       LEFT JOIN orders
              ON customers.customerid = orders.customerid
GROUP  BY customers.customerid,
          customers.NAME;


SELECT customers.NAME,
       orders.orderid,
       orders.orderdate
FROM   customers
       LEFT JOIN orders
              ON customers.customerid = orders.customerid;

SELECT *
FROM   books
WHERE  bookid NOT IN (SELECT DISTINCT bookid
                      FROM   orderitems);

SELECT books.title,
       orderitems.quantity
FROM   orderitems
       JOIN books
         ON orderitems.bookid = books.bookid;

SELECT customers.NAME
FROM   customers
       LEFT JOIN orders
              ON customers.customerid = orders.customerid;

SELECT authors.NAME
FROM   authors
       LEFT JOIN books
              ON authors.authorid = books.authorid
WHERE  books.bookid IS NULL; 