from .vector_store import VectorStore


class Retriever:

    def __init__(self):

        self.store = VectorStore()


    def search(self, query):

        results = self.store.collection.query(
            query_texts=[query],
            n_results=3
        )

        return results["documents"][0]