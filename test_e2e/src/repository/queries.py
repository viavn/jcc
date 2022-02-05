import uuid
import psycopg2
from psycopg2 import Error

from repository.config import db_name, db_user, db_port, db_password, db_host


def open_connection():
    return psycopg2.connect(
        user=db_user,
        password=db_password,
        host=db_host,
        port=db_port,
        database=db_name)


def get_child_by_name(child_name):
    try:
        connection = open_connection()
        cursor = connection.cursor()
        cursor.execute("SELECT * FROM children WHERE name = %s;", (child_name,))
        record = cursor.fetchone()
        return record

    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()


def delete_godparent(child_id):
    try:
        connection = open_connection()
        cursor = connection.cursor()
        cursor.execute('DELETE FROM god_parents WHERE god_parents."ChildId" = %s;', (child_id,))
        connection.commit()
    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()


def delete_user(user_login):
    try:
        connection = open_connection()
        cursor = connection.cursor()
        cursor.execute("DELETE FROM users WHERE login = %s;", (user_login,))
        connection.commit()
    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()


def reset_user_password(user_login):
    try:
        connection = open_connection()
        cursor = connection.cursor()
        cursor.execute("UPDATE users SET password = %s WHERE login = %s;", ('123', user_login,))
        connection.commit()
    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()


def insert_user(login, name, password, user_type_id):
    try:
        connection = open_connection()
        cursor = connection.cursor()
        cursor.execute(
            "INSERT INTO users(id, login, name, password, user_type_id, is_deleted)"
            "VALUES (%s, %s, %s, %s, %s, %s)",
            (str(uuid.uuid4()), login, name, password, user_type_id, False)
        )
        connection.commit()
    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()
