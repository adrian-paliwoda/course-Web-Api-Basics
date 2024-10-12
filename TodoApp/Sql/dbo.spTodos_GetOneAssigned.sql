USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_GetOneAssigned] Script Date: 6/18/2022 4:16:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_GetOneAssigned]
	@AssignedTo INT,
	@TodoId INT
AS
BEGIN
	SELECT Top 1
		Id,
		Task,
		AssignedTo,
		IsComplete
	FROM
		dbo.Todos
	WHERE
		AssignedTo = @AssignedTo 
		AND Id = @TodoId;
END
