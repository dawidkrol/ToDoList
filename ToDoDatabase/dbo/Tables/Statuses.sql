CREATE TABLE [dbo].[Statuses]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(10) NOT NULL, 
    [IsAvailable] BIT NOT NULL
)
