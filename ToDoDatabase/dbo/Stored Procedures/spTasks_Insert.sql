CREATE PROCEDURE [dbo].[spTasks_Insert]
	@Title nvarchar(50),
	@Description nvarchar(MAX),
	@UserId NVARCHAR (128),
	@StatusId int
AS
BEGIN
	INSERT INTO [dbo].[Tasks](Title,Description,CreatingDate,LastModifiedDate,UserId,Status, IsAvailable)
	VALUES(@Title,@Description,GETDATE(),GETDATE(),@UserId,@StatusId,1);
END
