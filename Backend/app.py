from fastapi import FastAPI
from routes import router

app = FastAPI(title="AI NPC Backend")

@app.get("/")
async def root():
    return {"message": "Server is running!"}

app.include_router(router)