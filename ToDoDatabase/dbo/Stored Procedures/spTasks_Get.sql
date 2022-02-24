CREATE PROCEDURE [dbo].[spTasks_Get]
	@UserId NVARCHAR (128)
AS
BEGIN
	set nocount on;

	SELECT [t].[Id], [t].[Title], [t].[Description], [t].[CreatingDate], [s].[Id], [s].[Title]
		FROM [dbo].[Tasks] as t LEFT JOIN [dbo].[Statuses] as s on s.Id = t.Status
		WHERE t.UserId = @UserId AND t.IsAvailable = 1 AND t.IsAvailable = 1
		ORDER BY [t].[LastModifiedDate] DESC
END
