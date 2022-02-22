CREATE PROCEDURE [dbo].[spTasks_ChangeAvailability]
	@Id int,
	@Avaliable bit
AS
BEGIN
	UPDATE Tasks
	SET IsAvailable = @Avaliable
	WHERE @Id = Id;
END
