
CREATE DATABASE [RoleProductDb];


USE [RoleProductDb];
GO

/* -------------------------
   ASP.NET Identity tables
   ------------------------- */

-- Roles
IF OBJECT_ID('dbo.AspNetRoles') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
    [Id]               NVARCHAR(450) NOT NULL PRIMARY KEY,
    [Name]             NVARCHAR(256) NULL,
    [NormalizedName]   NVARCHAR(256) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL
);
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON dbo.AspNetRoles([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END
GO

-- Users
IF OBJECT_ID('dbo.AspNetUsers') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
    [Id]                   NVARCHAR(450) NOT NULL PRIMARY KEY,
    [UserName]             NVARCHAR(256) NULL,
    [NormalizedUserName]   NVARCHAR(256) NULL,
    [Email]                NVARCHAR(256) NULL,
    [NormalizedEmail]      NVARCHAR(256) NULL,
    [EmailConfirmed]       BIT NOT NULL DEFAULT 0,
    [PasswordHash]         NVARCHAR(MAX) NULL,
    [SecurityStamp]        NVARCHAR(MAX) NULL,
    [ConcurrencyStamp]     NVARCHAR(MAX) NULL,
    [PhoneNumber]          NVARCHAR(MAX) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0,
    [TwoFactorEnabled]     BIT NOT NULL DEFAULT 0,
    [LockoutEnd]           DATETIMEOFFSET NULL,
    [LockoutEnabled]       BIT NOT NULL DEFAULT 0,
    [AccessFailedCount]    INT NOT NULL DEFAULT 0
);
CREATE NONCLUSTERED INDEX [EmailIndex] ON dbo.AspNetUsers([NormalizedEmail]);
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON dbo.AspNetUsers([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END
GO

-- Role claims
IF OBJECT_ID('dbo.AspNetRoleClaims') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetRoleClaims](
    [Id]      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [RoleId]  NVARCHAR(450) NOT NULL,
    [ClaimType]  NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AspNetRoleClaims_Role FOREIGN KEY([RoleId]) REFERENCES dbo.AspNetRoles([Id]) ON DELETE CASCADE
);
CREATE INDEX IX_AspNetRoleClaims_RoleId ON dbo.AspNetRoleClaims([RoleId]);
END
GO

-- User claims
IF OBJECT_ID('dbo.AspNetUserClaims') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AspNetUserClaims_User FOREIGN KEY([UserId]) REFERENCES dbo.AspNetUsers([Id]) ON DELETE CASCADE
);
CREATE INDEX IX_AspNetUserClaims_UserId ON dbo.AspNetUserClaims([UserId]);
END
GO

-- User logins
IF OBJECT_ID('dbo.AspNetUserLogins') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [ProviderKey]   NVARCHAR(450) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX) NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_User FOREIGN KEY([UserId]) REFERENCES dbo.AspNetUsers([Id]) ON DELETE CASCADE
);
CREATE INDEX IX_AspNetUserLogins_UserId ON dbo.AspNetUserLogins([UserId]);
END
GO

-- User roles
IF OBJECT_ID('dbo.AspNetUserRoles') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_User FOREIGN KEY([UserId]) REFERENCES dbo.AspNetUsers([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_Role FOREIGN KEY([RoleId]) REFERENCES dbo.AspNetRoles([Id]) ON DELETE CASCADE
);
CREATE INDEX IX_AspNetUserRoles_RoleId ON dbo.AspNetUserRoles([RoleId]);
END
GO

-- User tokens
IF OBJECT_ID('dbo.AspNetUserTokens') IS NULL
BEGIN
CREATE TABLE [dbo].[AspNetUserTokens](
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(450) NOT NULL,
    [Value] NVARCHAR(MAX) NULL,
    CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_User FOREIGN KEY([UserId]) REFERENCES dbo.AspNetUsers([Id]) ON DELETE CASCADE
);
END
GO

/* -------------------------
   Products table
   ------------------------- */
IF OBJECT_ID('dbo.Products') IS NULL
BEGIN
CREATE TABLE [dbo].[Products](
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(200) NOT NULL,
    [ProtectedPrice] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
END
GO

PRINT 'Database and tables created successfully.';

