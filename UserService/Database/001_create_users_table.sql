CREATE TABLE IF NOT EXISTS users
(
    id SERIAL PRIMARY KEY,
    login VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL,
    name VARCHAR(100) NOT NULL,
    surname VARCHAR(100) NOT NULL,
    age INTEGER NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE UNIQUE INDEX IF NOT EXISTS index_users_login
    ON users (login) WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS index_users_name_surname
    ON users (name, surname) WHERE is_deleted = FALSE;
