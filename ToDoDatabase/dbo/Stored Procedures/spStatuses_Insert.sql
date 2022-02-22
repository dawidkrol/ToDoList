CREATE PROCEDURE [dbo].[spStatuses_Insert]
	@Title NVARCHAR(10)
AS
BEGIN
	INSERT INTO [dbo].[Statuses](Title, IsAvailable) VALUES(@Title, 1)
	SELECT @@IDENTITY
END
