CREATE OR REPLACE FUNCTION fn_create_user
(
    p_login VARCHAR,
    p_password VARCHAR,
    p_name VARCHAR,
    p_surname VARCHAR,
    p_age INTEGER
)
RETURNS TABLE (id INTEGER, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INTEGER)
LANGUAGE sql
AS $$
    INSERT INTO users (login, password, name, surname, age)
    VALUES (p_login, p_password, p_name, p_surname, p_age)
    ON CONFLICT (login) WHERE is_deleted = FALSE DO NOTHING
    RETURNING users.id, users.login, users.password, users.name, users.surname, users.age;
$$;
