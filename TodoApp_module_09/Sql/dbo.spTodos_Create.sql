USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Create] Script Date: 6/18/2022 4:15:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTodos_Create]
	@Task NVARCHAR(50),
	@AssignedTo INT
AS
BEGIN
	INSERT INTO dbo.Todos (Task, AssignedTo)
	VALUES (@Task, @AssignedTo);

	SELECT
		Id,
		Task,
		AssignedTo,
		IsComplete
	FROM dbo.Todos
	WHERE
		Id = scope_identity();
END
