CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreatingDate] DATETIME NOT NULL, 
    [Deadline] NCHAR(10) NULL, 
    [UserId] NVARCHAR (128) NOT NULL, 
    [Status] INT NOT NULL DEFAULT 1,

)
