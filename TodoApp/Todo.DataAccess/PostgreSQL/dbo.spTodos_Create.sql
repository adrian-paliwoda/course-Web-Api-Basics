CREATE
    OR REPLACE FUNCTION spTodos_Create(
    task VARCHAR(50),
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
AS
$$
INSERT INTO Todos (Task, AssignedTo, IsComplete)
VALUES (task, assignedTo, BIT '0')
RETURNING id;   

SELECT Id,
       Task,
       AssignedTo,
       IsComplete
FROM Todos
WHERE Id = id;
$$;