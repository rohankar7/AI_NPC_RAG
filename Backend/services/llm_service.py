from pathlib import Path
import ollama
from .memory_service import MemoryService
from .memory_extractor import MemoryExtractor
from rag.retriever import Retriever

PROMPT_DIR = Path(__file__).parent.parent / "prompts"
MAX_HISTORY = 20

def load_prompt(filename: str) -> str:
        return (PROMPT_DIR / filename).read_text(encoding="utf-8")

class LLMService:

    def __init__(self):
        self.model = "qwen2.5:3b"
        self.memory = MemoryService()
        self.memory_extractor = MemoryExtractor()
        self.retriever = Retriever()
        self.system_prompt = load_prompt("system_prompt.txt")
        self.blacksmith_prompt = load_prompt("blacksmith.txt")
        self.merchant_prompt = load_prompt("merchant.txt")
        self.guard_prompt = load_prompt("guard.txt")
        self.conversations = {
            "blacksmith": [
                {
                    "role": "system",
                    "content": ""
                }
            ],
            "merchant": [
                {
                    "role": "system",
                    "content": ""
                }
            ],
            "guard": [
                {
                    "role": "system",
                    "content": ""
                }
            ]
        }

        self.npcs = {
            "blacksmith": self.blacksmith_prompt,
            "merchant": self.merchant_prompt,
            "guard": self.guard_prompt
        }

    def chat(self, message: str, npc_id: str) -> str:


        npc_prompt = self.npcs[npc_id]
        messages = self.conversations[npc_id]
        memory_result = self.memory_extractor.extract(message)
        print(memory_result)
        memories = self.memory.get_memories()
        if (
            memory_result
            and memory_result.get("remember")
            and memory_result.get("memory")
            and memory_result["memory"] not in memories
        ):
            self.memory.save_memory(memory_result["memory"].strip())
            print(self.memory.get_memories())
            memories.append(memory_result["memory"].strip())

        memory_context = "\n".join(memories)

        lore = self.retriever.search(message)
        lore_context = "\n".join(lore)
        messages[0]["role"] = "system"
        messages[0]["content"] = f"""
        {self.system_prompt}

        {npc_prompt}

        Known facts about the player:
        {memory_context}

        World knowledge:
        {lore_context}
        """
        messages.append({
            "role": "user",
            "content": message,  
        })
        response = ollama.chat(
            model = self.model,
            messages = messages
        )
        assistant_reply = response['message']['content']
        
        messages.append({
            "role": "assistant",
            "content": assistant_reply
        })
        if len(messages) > MAX_HISTORY + 1:
            messages = [
                messages[0],      # Keep system prompt
                *messages[-MAX_HISTORY:]
            ]
        return assistant_reply