CREATE OR REPLACE FUNCTION fn_get_user_by_id
(
    p_id INTEGER
)
RETURNS TABLE (id INTEGER, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INTEGER)
LANGUAGE sql
AS $$
    SELECT users.id, users.login, users.password, users.name, users.surname, users.age
    FROM users
    WHERE users.id = p_id AND users.is_deleted = FALSE;
$$;
