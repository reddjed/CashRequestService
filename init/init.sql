USE [master]
GO
CREATE DATABASE [PrivatBankDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PrivatBankDb', FILENAME = N'/var/opt/mssql/data/PrivatBankDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PrivatBankDb_log', FILENAME = N'/var/opt/mssql/data/PrivatBankDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PrivatBankDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PrivatBankDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PrivatBankDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PrivatBankDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PrivatBankDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PrivatBankDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PrivatBankDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PrivatBankDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PrivatBankDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PrivatBankDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PrivatBankDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PrivatBankDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PrivatBankDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PrivatBankDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PrivatBankDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PrivatBankDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PrivatBankDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PrivatBankDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PrivatBankDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PrivatBankDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PrivatBankDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PrivatBankDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PrivatBankDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PrivatBankDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PrivatBankDb] SET RECOVERY FULL 
GO
ALTER DATABASE [PrivatBankDb] SET  MULTI_USER 
GO
ALTER DATABASE [PrivatBankDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PrivatBankDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PrivatBankDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PrivatBankDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PrivatBankDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PrivatBankDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PrivatBankDb', N'ON'
GO
ALTER DATABASE [PrivatBankDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [PrivatBankDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PrivatBankDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[CurrencyId] [uniqueidentifier] NOT NULL,
	[Currency] [nchar](3) NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deps](
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[DepartmentAddress] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Deps] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[RequestId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[CurrencyId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[InsDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Requests] ADD  CONSTRAINT [DF_Requests_InsDate]  DEFAULT (getdate()) FOR [InsDate]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_ClientId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_CurrenciesId] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_CurrenciesId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Deps] ([DepartmentId])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_DepartmentId]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRequestStatusByClientIdAndDepAddress]
    @ClientId UNIQUEIDENTIFIER,
    @DepartmentAddress NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @_DepartmentId UNIQUEIDENTIFIER;

	SELECT TOP 1 @_DepartmentId = DepartmentId FROM Deps WHERE DepartmentAddress = @DepartmentAddress;
    IF @_DepartmentId IS NULL
    BEGIN
        RAISERROR('@DepartmentAddress is null', 16, 1);
        RETURN;
    END;

    SELECT * FROM Requests
	INNER JOIN Clients ON Requests.ClientId = Clients.ClientId
	INNER JOIN Currencies ON Requests.CurrencyId = Currencies.CurrencyId
	INNER JOIN Deps ON Requests.DepartmentId = Deps.DepartmentId
    WHERE Requests.ClientId = @ClientId AND Requests.DepartmentId = @_DepartmentId;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRequestStatusById]
    @RequestId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Requests
	INNER JOIN Clients ON Requests.ClientId = Clients.ClientId
	INNER JOIN Currencies ON Requests.CurrencyId = Currencies.CurrencyId
	INNER JOIN Deps ON Requests.DepartmentId = Deps.DepartmentId
    WHERE RequestId = @RequestId;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertRequest]
    @ClientId UNIQUEIDENTIFIER,
    @DepartmentAddress NVARCHAR(255),
    @Amount DECIMAL(18, 2),
    @Currency NVARCHAR(3),
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @_ClientId UNIQUEIDENTIFIER;
    DECLARE @_DepartmentId UNIQUEIDENTIFIER;
    DECLARE @_CurrencyId UNIQUEIDENTIFIER;
    DECLARE @_RequestId UNIQUEIDENTIFIER;

    SELECT TOP 1 @_ClientId = ClientId FROM Clients WHERE ClientId = @ClientId;
    IF @_ClientId IS NULL
    BEGIN
        RAISERROR('@ClientId is null', 16, 1);
        RETURN;
    END;

    SELECT TOP 1 @_DepartmentId = DepartmentId FROM Deps WHERE DepartmentAddress = @DepartmentAddress;
    IF @_DepartmentId IS NULL
    BEGIN
        RAISERROR('@DepartmentAddress is null', 16, 1);
        RETURN;
    END;

    SELECT TOP 1 @_CurrencyId = CurrencyId FROM Currencies WHERE Currency = @Currency;
    IF @_CurrencyId IS NULL
    BEGIN
        RAISERROR('@Currency is null', 16, 1);
        RETURN;
    END;
	
    SET @_RequestId = NEWID();
    INSERT INTO Requests (RequestId, ClientId, DepartmentId, Amount, CurrencyId, Status)
    VALUES (@_RequestId, @_ClientId, @_DepartmentId, @Amount, @_CurrencyId, @Status);
    
    SELECT @_RequestId AS NewRequestId;
END;
GO
USE [master]
GO
ALTER DATABASE [PrivatBankDb] SET  READ_WRITE 
GO
