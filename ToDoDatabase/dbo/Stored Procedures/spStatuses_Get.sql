﻿CREATE PROCEDURE [dbo].[spStatuses_Get]
AS
BEGIN
	set nocount on;

	SELECT s.Id, s.Title from Statuses as s;
END
