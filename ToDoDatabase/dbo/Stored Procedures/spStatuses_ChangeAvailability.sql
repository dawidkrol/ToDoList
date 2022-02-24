CREATE PROCEDURE [dbo].[spStatuses_ChangeAvailability]
	@Id int,
	@Avaliable bit,
	@UserId NVARCHAR (128)
AS
BEGIN
	IF((SELECT count(*) FROM [dbo].[Statuses] as s where s.UserId = @UserId AND s.Id = @Id) = 1)
		UPDATE Statuses
		SET IsAvailable = @Avaliable
		WHERE @Id = Id AND @UserId = UserId;

	ELSE
		THROW 77777, 'Cannot find your status', 1;
END