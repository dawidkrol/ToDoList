CREATE PROCEDURE [dbo].[spTasks_UpdateStatus]
	@Id int,
	@StatusId int
AS
BEGIN
	UPDATE Tasks
	SET Status = @StatusId
	WHERE @Id = Id;
END
