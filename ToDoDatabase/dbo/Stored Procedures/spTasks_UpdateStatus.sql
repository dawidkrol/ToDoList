CREATE PROCEDURE [dbo].[spTasks_UpdateStatus]
	@Id int,
	@StatusId int,
	@UserId NVARCHAR (128)
AS
BEGIN
	UPDATE Tasks
	SET Status = @StatusId
	WHERE @Id = Id AND @UserId = UserId;
END