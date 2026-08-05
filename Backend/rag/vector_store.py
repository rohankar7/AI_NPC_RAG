from pathlib import Path
import chromadb
from sentence_transformers import SentenceTransformer


class VectorStore:

    def __init__(self):

        self.client = chromadb.PersistentClient(
            path="./chroma_db"
        )

        self.collection = self.client.get_or_create_collection(
            name="world_lore"
        )

        self.embedding_model = SentenceTransformer(
            "BAAI/bge-small-en-v1.5"
        )


    def add_documents(self):

        lore_path = Path("../lore")

        documents = []

        for file in lore_path.glob("*.txt"):
            text = file.read_text(
                encoding="utf-8"
            )

            documents.append(text)


        embeddings = self.embedding_model.encode(
            documents
        ).tolist()


        self.collection.add(
            documents=documents,
            embeddings=embeddings,
            ids=[
                str(i)
                for i in range(len(documents))
            ]
        )