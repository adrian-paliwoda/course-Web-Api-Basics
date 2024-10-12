CREATE
OR REPLACE PROCEDURE spTodos_CompleteTodo(
	assignedTo INT,
	todoId INT
)
LANGUAGE SQL
AS $$
UPDATE Todos
SET IsComplete = B'1'
WHERE Id = todoId
  AND AssignedTo = assignedTo;
$$;
