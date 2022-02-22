CREATE PROCEDURE [dbo].[spTasks_Insert]
	@Title nvarchar(50),
	@Description nvarchar(MAX),
	@Deadline DateTime2,
	@UserId NVARCHAR (128),
	@StatusId int
AS
BEGIN
	INSERT INTO [dbo].[Tasks](Title,Description,CreatingDate,LastModifiedDate,Deadline,UserId,Status, IsAvailable)
	VALUES(@Title,@Description,GETDATE(),GETDATE(),@Deadline,@UserId,@StatusId,1);
END
