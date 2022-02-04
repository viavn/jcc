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
        # Connect to an existing database
        connection = open_connection()

        # Create a cursor to perform database operations
        cursor = connection.cursor()
        # Print PostgreSQL details
        print("PostgreSQL server information")
        print(connection.get_dsn_parameters(), "\n")
        # Executing a SQL query
        cursor.execute("SELECT * FROM children WHERE name = %s;", (child_name,))
        # Fetch result
        record = cursor.fetchone()
        print("You are connected to - ", record, "\n")
        return record

    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()
            print("PostgreSQL connection is closed")


def delete_godparent(child_id):
    try:
        # Connect to an existing database
        connection = open_connection()

        # Create a cursor to perform database operations
        cursor = connection.cursor()
        cursor.execute('DELETE FROM god_parents WHERE god_parents."ChildId" = %s;', (child_id,))
        connection.commit()
        count = cursor.rowcount
        print(count, "Record deleted successfully")

    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()
            print("PostgreSQL connection is closed")


def delete_user(user_login):
    try:
        # Connect to an existing database
        connection = open_connection()

        # Create a cursor to perform database operations
        cursor = connection.cursor()
        cursor.execute("DELETE FROM users WHERE login = %s;", (user_login,))
        connection.commit()
        count = cursor.rowcount
        print(count, "Record deleted successfully ")

    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()
            print("PostgreSQL connection is closed")


def reset_user_password(user_login):
    try:
        # Connect to an existing database
        connection = open_connection()

        # Create a cursor to perform database operations
        cursor = connection.cursor()
        cursor.execute("UPDATE users SET password = %s WHERE login = %s;", ('123', user_login,))
        connection.commit()
        count = cursor.rowcount
        print(count, "Record updated successfully ")

    except (Exception, Error) as error:
        print("Error while connecting to PostgreSQL", error)
    finally:
        if connection:
            cursor.close()
            connection.close()
            print("PostgreSQL connection is closed")
