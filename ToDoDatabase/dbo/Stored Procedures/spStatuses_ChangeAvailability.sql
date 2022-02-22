CREATE PROCEDURE [dbo].[spStatuses_ChangeAvailability]
	@Id int,
	@Avaliable bit
AS
BEGIN
	UPDATE Statuses
	SET IsAvailable = @Avaliable
	WHERE @Id = Id;
END
