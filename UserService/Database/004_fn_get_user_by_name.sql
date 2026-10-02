CREATE OR REPLACE FUNCTION fn_get_user_by_name
(
    p_name VARCHAR,
    p_surname VARCHAR
)
RETURNS TABLE (id INTEGER, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INTEGER)
LANGUAGE sql
AS $$
    SELECT users.id, users.login, users.password, users.name, users.surname, users.age
    FROM users
    WHERE users.name = p_name AND users.surname = p_surname AND users.is_deleted = FALSE
    ORDER BY users.id
    LIMIT 1;
$$;
