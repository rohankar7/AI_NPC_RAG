from services.memory_service import MemoryService

memory = MemoryService()

# memory.save_memory(
#     "Player's dog's name is Luna"
# )

print(memory.get_memories())