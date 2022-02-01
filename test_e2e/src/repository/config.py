import os

db_host = os.environ.get('DB_HOST', default='localhost')
db_name = os.environ.get('DB_NAME', default='jcc_db')
db_user = os.environ.get('DB_USER', default='postgres')
db_password = os.environ.get('DB_PASSWORD', default='YOUR_PASSWORD')
db_port = os.environ.get('DB_PORT', default='5432')
