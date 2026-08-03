from pathlib import Path
import ollama

PROMPT_DIR = Path(__file__).parent.parent / "prompts"

def load_prompt(filename: str) -> str:
        return (PROMPT_DIR / filename).read_text(encoding="utf-8")

class LLMService:

    def __init__(self):
        self.model = "qwen2.5:3b"
        self.system_prompt = load_prompt("system_prompt.txt")
        self.blacksmith_prompt = load_prompt("blacksmith.txt")
        self.merchant_prompt = load_prompt("merchant.txt")
        self.guard_prompt = load_prompt("guard.txt")

    def chat(self, message: str) -> str:
        response = ollama.chat(
            model = self.model,
            messages=[
                {
                    "role": "system",
                    "content": self.system_prompt,
                },
                {
                    "role": "user",
                    "content": message
                }
            ]
        )
        return response.message.content