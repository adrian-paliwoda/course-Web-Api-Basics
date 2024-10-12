CREATE TABLE Todos
(
    Id         INT GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
    Task       CHARACTER(50)                    NOT NULL,
    AssignedTo INT                              NOT NULL,
    IsComplete BIT                              NOT NULL
);
    