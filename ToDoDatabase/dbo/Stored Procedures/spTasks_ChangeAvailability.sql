CREATE PROCEDURE [dbo].[spTasks_ChangeAvailability]
	@Id int,
	@Avaliable bit,
	@UserId NVARCHAR (128)
AS
BEGIN
	UPDATE Tasks
	SET IsAvailable = @Avaliable
	WHERE @Id = Id AND @UserId = UserId;
END
