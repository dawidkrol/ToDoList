CREATE PROCEDURE [dbo].[spStatuses_Insert]
	@Title NVARCHAR(10)
AS
BEGIN
	INSERT INTO [dbo].[Statuses](Title) VALUES(@Title)
	SELECT @@IDENTITY
END
