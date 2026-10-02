CREATE OR REPLACE FUNCTION fn_update_user
(
    p_id INTEGER,
    p_password VARCHAR,
    p_name VARCHAR,
    p_surname VARCHAR,
    p_age INTEGER
)
RETURNS TABLE (id INTEGER, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INTEGER)
LANGUAGE sql
AS $$
    UPDATE users
    SET password = COALESCE(p_password, password),
        name = COALESCE(p_name, name),
        surname = COALESCE(p_surname, surname),
        age = COALESCE(p_age, age)
    WHERE users.id = p_id AND users.is_deleted = FALSE
    RETURNING users.id, users.login, users.password, users.name, users.surname, users.age;
$$;
