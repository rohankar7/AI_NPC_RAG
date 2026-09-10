Yep — I’d cut the README down **a lot**. The current one is too long for a portfolio GitHub repo. Keep the architecture, features, stack, setup, and roadmap; remove most of the explanatory repetition.

# 🤖 AI NPC RAG

> **Persistent, RAG-powered AI NPCs for Unity.**

AI NPC RAG is a **local-first AI NPC system** combining LLMs, persistent memory, RAG, and Unity game-world interaction.

NPCs can remember players, retrieve world knowledge, maintain distinct personalities, and eventually **take actions inside the game**.

## 🚧 Project Status

**Prototype / MVP — Active Development**

Core pipeline is working:

```text
Unity → FastAPI → Memory + RAG → Ollama → NPC
```

Currently building **AI tool/function calling** so NPCs can interact with the Unity game world.

---

## ✨ Features

* 🧠 **Persistent Memory** — NPCs remember important player information using SQLite.
* 👥 **Cross-NPC Memory** — Player memories are shared between NPCs.
* 🎭 **Multiple NPC Personalities** — Independent prompts and conversation histories.
* 📚 **RAG** — NPCs retrieve relevant world lore using ChromaDB.
* 🔎 **Semantic Retrieval** — Sentence Transformers for embedding and retrieval.
* 🏠 **Local-First** — Runs locally using Ollama without paid LLM APIs.
* 🧩 **Modular Backend** — Separate services for LLM, memory, retrieval, and game interaction.

### Current NPCs

| NPC       | Role       | Personality             |
| --------- | ---------- | ----------------------- |
| 🔨 Rowan  | Blacksmith | Gruff, skilled, helpful |
| 💰 Mira   | Merchant   | Charismatic, talkative  |
| 🛡️ Alden | Guard      | Serious, suspicious     |

---

## 🏗️ Architecture

```text
                    Unity
                      │
                      ▼
                   FastAPI
                      │
          ┌───────────┼───────────┐
          ▼           ▼           ▼
       Memory        RAG       Tools
       SQLite      ChromaDB    Functions
          │           │           │
          └───────────┼───────────┘
                      ▼
                    Ollama
                      │
                      ▼
                   Qwen 2.5
                      │
                      ▼
                  NPC Response
```

### Backend

```text
Backend/
├── services/
│   ├── llm_service.py
│   ├── memory_service.py
│   └── memory_extractor.py
├── rag/
│   ├── vector_store.py
│   └── retriever.py
├── prompts/
├── lore/
└── routes.py
```

---

## 🛠️ Tech Stack

| Technology            | Purpose           |
| --------------------- | ----------------- |
| Unity / C#            | Game client       |
| Python                | AI backend        |
| FastAPI               | API               |
| Ollama                | Local inference   |
| Qwen 2.5 3B           | LLM               |
| SQLite                | Persistent memory |
| ChromaDB              | Vector retrieval  |
| Sentence Transformers | Embeddings        |

---

## 🚀 Setup

### Requirements

* Unity
* Python 3.10+
* Ollama
* Git

### Install dependencies

```bash
pip install -r requirements.txt
```

### Pull the model

```bash
ollama pull qwen2.5:3b
```

### Start the backend

```bash
uvicorn app:app --reload
```

API:

```text
http://127.0.0.1:8000
```

Docs:

```text
http://127.0.0.1:8000/docs
```

Then open the Unity project and enter **Play Mode**.

---

## 💬 Example

```text
Player:
"My name is Rohan and I love swords."

        ↓

Memory Extractor

        ↓

SQLite

"Player's name is Rohan"
"Player loves swords"

        ↓

Later...

Player → Mira:
"What's my name?"

Mira:
"Rohan, right?"
```

NPCs can also retrieve world information through RAG:

```text
Player:
"Who rules the Northern Kingdom?"

        ↓

ChromaDB → Relevant Lore

        ↓

Qwen → NPC Response
```

---

## 🎯 Roadmap

### ✅ Completed

* [x] Unity ↔ FastAPI communication
* [x] Local LLM inference
* [x] Multiple NPC personalities
* [x] Independent conversations
* [x] Persistent SQLite memory
* [x] ChromaDB RAG
* [x] Local-first architecture

### 🚧 Next

* [ ] Tool / function calling
* [ ] AI-controlled Unity actions
* [ ] Game-state awareness
* [ ] NPC-specific memory
* [ ] Agent planning
* [ ] Better memory retrieval

### 🔮 Future

* [ ] Multi-agent interactions
* [ ] Voice
* [ ] Agent evaluation
* [ ] Generic agent API
* [ ] Unity package
* [ ] Support for non-Unity environments

---

## 🧰 From NPC → Agent

The main goal is to move beyond text generation:

```text
LLM → Decision → Tool → Environment
```

For example:

```text
Player:
"Can I have an iron sword?"

        ↓

NPC → give_item()

        ↓

Unity Inventory

        ↓

Iron Sword Added
```

This architecture could extend beyond games to **simulations, training environments, and other interactive AI systems**.

---

## 🌍 Why?

The project explores how **LLMs + RAG + persistent memory + tools + environments** can create AI agents that do more than generate text.

The architecture is designed to be:

* **Local-first**
* **Modular**
* **Persistent**
* **Environment-aware**
* **Open source**

---

## 📸 Demo

> 🚧 Demo video coming soon.

Planned demo:

```text
Player introduces themselves
        ↓
NPC remembers them
        ↓
Talk to another NPC
        ↓
Shared memory
        ↓
Ask about world lore
        ↓
RAG retrieval
        ↓
NPC performs a game action
```

---

## 📄 License

See [`LICENSE`](LICENSE).

---

⭐ **If you find the project interesting, consider starring the repository or contributing.**