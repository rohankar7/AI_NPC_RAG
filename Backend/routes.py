from fastapi import APIRouter
from models import ChatRequest, ChatResponse
from services.llm_service import LLMService

router = APIRouter()
llm = LLMService()

@router.post("/chat", response_model=ChatResponse)

def chat(request: ChatRequest):
    # return ChatResponse(reply=f"NPC says: You said `{request.message}`")
    return ChatResponse(reply=llm.chat(request.message))