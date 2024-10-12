USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_GetAllAssigned] Script Date: 6/18/2022 4:16:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_GetAllAssigned]
	@AssignedTo INT
AS
BEGIN
	SELECT 
		Id,
		Task,
		AssignedTo,
		IsComplete
	FROM
		dbo.Todos
	WHERE
		AssignedTo = @AssignedTo;
END
