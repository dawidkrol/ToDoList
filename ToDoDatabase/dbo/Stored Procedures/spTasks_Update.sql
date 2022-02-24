CREATE PROCEDURE [dbo].[spTasks_Update]
	@Id int,
	@Title NVARCHAR(50),
	@Description NVARCHAR(MAX),
	@StatusId INT,
	@UserId NVARCHAR (128)
AS
BEGIN

	IF((SELECT count(*) FROM [dbo].[Statuses] as s where (s.UserId = @UserId OR s.UserId = 0) AND s.Id = @StatusId) = 1)
		UPDATE Tasks
		SET Title = @Title,
			Description = @Description,
			Status = @StatusId
		WHERE @Id = Id AND @UserId = UserId;

	ELSE
		THROW 77777, 'Cannot find your status', 1;
END
