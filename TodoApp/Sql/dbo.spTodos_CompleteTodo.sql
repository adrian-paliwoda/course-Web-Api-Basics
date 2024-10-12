USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_CompleteTodo] Script Date: 6/18/2022 4:15:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_CompleteTodo]
	@AssignedTo INT,
	@TodoId INT
AS
BEGIN
	UPDATE dbo.Todos
	SET 
		IsComplete = 1
	WHERE 
		Id = @TodoId 
		AND AssignedTo = @AssignedTo;
END
