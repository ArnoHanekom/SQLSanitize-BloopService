USE [tempdb]
GO

DECLARE @sql VARCHAR(MAX)
IF EXISTS (SELECT 1 FROM sys.databases WHERE [name] = 'BloopServiceDB')
BEGIN
	SET @sql = N'USE [BloopServiceDB];
	ALTER DATABASE [BloopServiceDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	USE [master];
	DROP DATABASE [BloopServiceDB];'
	
	EXEC (@sql);
	USE [tempdb];
END
GO

CREATE DATABASE [BloopServiceDB]
GO

USE [BloopServiceDB]
GO

CREATE TABLE [dbo].[Sensitivewords]
(
	[Id]				INT				IDENTITY(1,1)		NOT NULL,
	[WordDefinition]	VARCHAR(150)						NOT NULL,
	[CreatedOn]			DATETIME		DEFAULT(GETDATE())	NOT NULL,
	[ModifiedOn]		DATETIME							NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO