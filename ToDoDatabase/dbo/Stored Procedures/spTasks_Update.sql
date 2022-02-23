CREATE PROCEDURE [dbo].[spTasks_Update]
	@Id int,
	@Title NVARCHAR(50),
	@Description NVARCHAR(MAX),
	/*@Deadline DATETIME2,*/
	@StatusId INT,
	@UserId NVARCHAR (128)
AS
BEGIN
	UPDATE Tasks
	SET Title = @Title,
		Description = @Description,
		/*Deadline = @Deadline,*/
		Status = @StatusId
	WHERE @Id = Id AND @UserId = UserId;
END
