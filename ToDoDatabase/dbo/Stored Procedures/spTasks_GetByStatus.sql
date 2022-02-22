CREATE PROCEDURE [dbo].[spTasks_GetByStatus]
	@UserId NVARCHAR (128),
	@StatusId NVARCHAR(10)
AS
BEGIN
	set nocount on;

	SELECT [t].[Id], [t].[Title], [t].[Description], [t].[CreatingDate], [s].[Title] as 'Status'
		FROM [dbo].[Tasks] as t LEFT JOIN [dbo].[Statuses] as s on s.Id = [t].[Status]
		WHERE t.UserId = @UserId AND t.IsAvailable = 1 AND t.IsAvailable = 1 AND @StatusId = [t].[Status]
		ORDER BY [t].[LastModifiedDate] DESC;
END