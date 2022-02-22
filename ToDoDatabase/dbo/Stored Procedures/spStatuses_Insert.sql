CREATE PROCEDURE [dbo].[spStatuses_Insert]
	@UserId NVARCHAR (128),
	@Title NVARCHAR(10)
AS
BEGIN
	INSERT INTO [dbo].[Statuses](Title, IsAvailable, UserId)
		VALUES(@Title, 1, @UserId);
	SELECT @@IDENTITY
END
