CREATE PROCEDURE [dbo].[spTasks_Get]
	@Id NVARCHAR (128)
AS
BEGIN
	set nocount on;

	SELECT [t].[Id], [t].[Title], [t].[Description], [t].[CreatingDate], [s].[Title] as 'Status'
		from [dbo].[Tasks] as t LEFT JOIN [dbo].[Statuses] as s on s.Id = t.Status
		WHERE t.UserId = @Id AND t.IsAvailable = 1 AND t.IsAvailable = 1
		ORDER BY [t].[LastModifiedDate] DESC
END
