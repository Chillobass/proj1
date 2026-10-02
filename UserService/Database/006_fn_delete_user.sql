CREATE OR REPLACE FUNCTION fn_delete_user
(
    p_id INTEGER
)
RETURNS TABLE (id INTEGER)
LANGUAGE sql
AS $$
    UPDATE users
    SET is_deleted = TRUE
    WHERE users.id = p_id AND users.is_deleted = FALSE
    RETURNING users.id;
$$;
