IF DB_ID('SmartHRMDb') IS NULL
BEGIN
    CREATE DATABASE SmartHRMDb;
END
GO


USE SmartHRMDb;
GO

-- Create Roles table if not exists
IF OBJECT_ID('tblRoles', 'U') IS NULL
CREATE TABLE tblRoles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Organizations table if not exists
IF OBJECT_ID('tblOrganizations', 'U') IS NULL
CREATE TABLE tblOrganizations (
    OrganizationId INT IDENTITY(1,1) PRIMARY KEY,
    OrganizationName NVARCHAR(255) NOT NULL,
    OrganizationCode NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Users table if not exists
IF OBJECT_ID('tblUsers', 'U') IS NULL
CREATE TABLE tblUsers (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    RoleId INT NOT NULL,
    OrganizationId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES tblRoles(RoleId) ON DELETE CASCADE,
    FOREIGN KEY (OrganizationId) REFERENCES tblOrganizations(OrganizationId) ON DELETE CASCADE
);

-- Insert default roles if empty
IF NOT EXISTS (SELECT 1 FROM tblRoles)
INSERT INTO tblRoles (RoleName, Description) VALUES
('Employee', 'Regular user with limited access'),
('Manager', 'Manages teams and operations'),
('HR/Admin', 'HR and Admin access'),
('Super Admin', 'Full system access'),
('Organization', 'Organization account');

-- Insert a default organization if empty
IF NOT EXISTS (SELECT 1 FROM tblOrganizations)
INSERT INTO tblOrganizations (OrganizationName, OrganizationCode) VALUES
('Default Organization', 'DEF001');

-- Show all tables
SELECT * FROM sys.tables;
SELECT * FROM sys.tables;

ALTER TABLE tblUsers
ADD OrganizationId INT NOT NULL DEFAULT 1;  

ALTER TABLE tblUsers
ADD CONSTRAINT FK_tblUsers_tblOrganizations
FOREIGN KEY (OrganizationId) REFERENCES tblOrganizations(OrganizationId) ON DELETE CASCADE;