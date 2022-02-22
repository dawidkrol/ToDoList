CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreatingDate] DATETIME2 NOT NULL, 
    [LastModifiedDate] DATETIME2 NOT NULL,
    [Deadline] DATETIME2 NULL, 
    [UserId] NVARCHAR (128) NOT NULL, 
    [Status] INT NOT NULL, 
    [IsAvailable] BIT NOT NULL

)
