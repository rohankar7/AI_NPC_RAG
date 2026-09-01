from pydantic import BaseModel

class ChatRequest(BaseModel):
    message: str
    npc_id: str

class ChatResponse(BaseModel):
    reply: str