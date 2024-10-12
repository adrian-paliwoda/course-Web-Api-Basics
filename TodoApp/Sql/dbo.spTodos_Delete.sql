USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Delete] Script Date: 6/18/2022 4:15:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_Delete]
	@AssignedTo INT,
	@TodoId INT
AS
BEGIN
	DELETE FROM dbo.Todos
	WHERE 
		Id = @TodoId 
		AND AssignedTo = @AssignedTo;
END
