import sys
import os

sys.path.append(
    os.path.dirname(
        os.path.dirname(
            os.path.abspath(__file__)
        )
    )
)
from database.database import get_connection

class MemoryService:

    def __init__(self):
        self.create_table()
    
    def create_table(self):
        connection = get_connection()
        cursor = connection.cursor()

        cursor.execute("""
            CREATE TABLE IF NOT EXISTS memories (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            content TEXT NOT NULL,
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            )
        """)

        connection.commit()
        connection.close()
    
    def save_memory(self, content: str):
        connection = get_connection()
        cursor = connection.cursor()

        cursor.execute(
            """
            INSERT INTO memories (content)
            VALUES (?)
            """,
            (content,)
        )
        connection.commit()
        connection.close()
    
    def get_memories(self):
        connection = get_connection()
        cursor = connection.cursor()

        cursor.execute(
            """
            SELECT content
            FROM memories
            ORDER BY created_at DESC
            """
        )

        results = cursor.fetchall()
        connection.close()

        return [memory[0] for memory in results]