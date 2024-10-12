CREATE OR REPLACE FUNCTION spTodos_GetOneAssigned(
    assignedTo INT,
    todoId INT
)
    RETURNS TABLE
            (
                Id         INT,
                Task       VARCHAR(50),
                AssignedTo INT,
                IsComplete BIT
            )
    LANGUAGE SQL
AS
$$
SELECT Id,
       Task,
       AssignedTo,
       IsComplete
FROM Todos t
WHERE t.AssignedTo = assignedTo
      AND t.Id = todoId
LIMIT 1;
$$;