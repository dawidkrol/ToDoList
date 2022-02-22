CREATE PROCEDURE [dbo].[spTasks_Insert]
	@Title nvarchar(50),
	@Description nvarchar(MAX),
	@Deadline DateTime2,
	@UserId NVARCHAR (128),
	@StatusTitle NVARCHAR(10)
AS
BEGIN
	/* TODO: Find better solution */
	DECLARE @StatusId as int;
	SET @StatusId = (SELECT s.Id FROM Statuses as s WHERE @StatusTitle = s.Title);


	INSERT INTO [dbo].[Tasks](Title,Description,CreatingDate,Deadline,UserId,Status)
	VALUES(@Title,@Description,GETDATE(),@Deadline,@UserId,@StatusId);
END
