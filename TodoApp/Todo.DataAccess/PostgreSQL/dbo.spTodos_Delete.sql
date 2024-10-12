CREATE OR REPLACE PROCEDURE spTodos_Delete(
    assignedTo INT,
    todoId INT
)
    LANGUAGE SQL
AS
$$
DELETE
FROM Todos
WHERE Id = todoId
  AND AssignedTo = assignedTo;
$$;
