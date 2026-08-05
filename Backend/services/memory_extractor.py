import ollama
import json


class MemoryExtractor:

    def __init__(self):
        self.model = "qwen2.5:3b"

    def extract(self, message: str):
        response = ollama.chat(
            model=self.model,
            messages=[
                {
                    "role": "system",
                    "content":
                    """
                    You are a memory extraction system for a game NPC.

                    Decide if the player's message contains information worth remembering.

                    Remember:
                    - Player name
                    - Family and friends
                    - Personal preferences
                    - Important events
                    - Promises
                    - Long-term goals

                    Do NOT remember:
                    - Greetings
                    - Casual conversation
                    - Temporary questions
                    - NPC dialogue

                    Return ONLY valid JSON.
                    No explanation.
                    No markdown.

                    Format:

                    {
                        "remember": true,
                        "memory": "short factual summary"
                    }

                    or

                    {
                        "remember": false,
                        "memory": ""
                    }
                    """
                },
                {
                    "role": "user",
                    "content": message
                }
            ]
        )
        content = response["message"]["content"].strip()

        if content.startswith("```"):
            content = content.replace("```json", "")
            content = content.replace("```", "")
            content = content.strip()
        try:
            data = json.loads(content)

            if "remember" not in data or "memory" not in data:
                return {
                    "remember": False,
                    "memory": ""
                }

            return data
        except json.JSONDecodeError:
            return {
                "remember": False,
                "memory": ""
            }