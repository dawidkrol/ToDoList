CREATE PROCEDURE [dbo].[spTasks_Insert]
	@Title nvarchar(50),
	@Description nvarchar(MAX),
	@StatusId int,
	@UserId NVARCHAR (128)
AS
BEGIN
	IF((SELECT count(*) FROM [dbo].[Statuses] as s where (s.UserId = @UserId OR s.UserId = 0) AND s.Id = @StatusId) = 1)
		INSERT INTO [dbo].[Tasks](Title,Description,CreatingDate,LastModifiedDate,UserId,Status, IsAvailable)
		VALUES(@Title,@Description,GETDATE(),GETDATE(),@UserId,@StatusId,1);

	ELSE
		THROW 77777, 'Cannot find your status', 1;
END
