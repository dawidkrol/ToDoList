CREATE PROCEDURE [dbo].[spStatuses_Get]
	@UserId NVARCHAR (128)
AS
BEGIN
	set nocount on;

	SELECT s.Id, s.Title from Statuses as s 
	WHERE s.IsAvailable = 1 AND (s.UserId = @UserId OR s.UserId = 0);
END
