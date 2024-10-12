USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_UpdateTask] Script Date: 6/18/2022 4:16:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_UpdateTask]
	@Task NVARCHAR(50),
	@AssignedTo INT,
	@TodoId INT
AS
BEGIN
	UPDATE dbo.Todos
	SET
		Task = @Task
	WHERE
		Id = @TodoId 
		AND AssignedTo = @AssignedTo;
END
