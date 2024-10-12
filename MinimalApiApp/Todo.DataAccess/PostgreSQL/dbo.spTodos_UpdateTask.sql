CREATE OR REPLACE PROCEDURE public.sptodos_updatetask(
    IN par_task CHARACTER VARYING,
    IN par_assignedto INTEGER,
    IN par_todoid INTEGER)
    LANGUAGE 'sql'
AS
$BODY$
UPDATE todos
SET task = par_task
WHERE id = par_todoid
  AND assignedto = par_assignedto;
$BODY$;