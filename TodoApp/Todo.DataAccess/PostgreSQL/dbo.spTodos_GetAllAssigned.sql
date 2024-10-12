CREATE
    OR REPLACE FUNCTION spTodos_GetAllAssigned(
    assignedTo INT   
)
    RETURNS TABLE
            (
                Id         INT,
                Task       VARCHAR(50),
                AssignedTo INT,
                IsComplete BIT
            )
LANGUAGE SQL
AS $$
SELECT Id,
       Task,
       AssignedTo,
       IsComplete
FROM Todos
WHERE AssignedTo = assignedTo;
$$;
