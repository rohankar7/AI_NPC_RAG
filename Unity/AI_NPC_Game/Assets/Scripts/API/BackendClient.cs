using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class BackendClient : MonoBehaviour
{
    private const string backendURL = "http://127.0.0.1:8000/chat";
    [Serializable] private class ChatRequest
    {
        public string message;
        public string npc_id;
    }
    [Serializable] public class ChatResponse
    {
        public string reply;
    }

    public void SendMessage(string message, string npc_id, Action<string> onSuccess)
    {
        StartCoroutine(PostMessage(message, npc_id, onSuccess));
    }

    private IEnumerator PostMessage(string message, string npc_id, Action<string> onSuccess)
    {
        ChatRequest chatRequest = new ChatRequest();
        chatRequest.message = message;
        chatRequest.npc_id = npc_id;
        string json = JsonUtility.ToJson(chatRequest);
        UnityWebRequest unityWebRequest = new UnityWebRequest(backendURL, "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(body);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(unityWebRequest.error);
            yield break;
        }
        ChatResponse response = JsonUtility.FromJson<ChatResponse>(unityWebRequest.downloadHandler.text);
        if (response == null || string.IsNullOrEmpty(response.reply))
        {
            Debug.LogError("Failed to parse backend response.");
            yield break;
        }
        onSuccess?.Invoke(response.reply);
    }
}