CREATE DATABASE InsuranceDB;
GO
USE InsuranceDB;
GO

CREATE TABLE Policies (
    PolicyID INT IDENTITY(1,1) PRIMARY KEY,
    PolicyHolderName VARCHAR(100) NOT NULL,
    PolicyType VARCHAR(50) CHECK (PolicyType IN ('Life', 'Health', 'Vehicle', 'Property')) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
);
	-- Type INT Null,

INSERT INTO Policies (PolicyHolderName, PolicyType, StartDate, EndDate)
VALUES 
    ('John Doe', 'Life', '2023-01-01', '2028-01-01'),
    ('Jane Smith', 'Health', '2022-06-15', '2027-06-15'),
    ('Alice Johnson', 'Vehicle', '2024-03-10', '2026-03-10'),
    ('Bob Brown', 'Property', '2023-09-20', '2029-09-20');

	drop table 
	select * from Policies;