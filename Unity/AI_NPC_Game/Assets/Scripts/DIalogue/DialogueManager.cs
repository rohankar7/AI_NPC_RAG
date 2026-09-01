using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private TMP_Text replyText;
    [SerializeField] private BackendClient backend;

    public void Send()
    {
        string message = messageInput.text;
        messageInput.text = "";
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        replyText.text = "Thinking...";
        try
        {
            backend.SendMessage(message, Player.playerInstance.npc_id, OnReply);
        }
        catch
        {
            Debug.LogError("No npc_id interacting with Player");
        }
    }

    private void OnReply(string reply)
    {
        Debug.Log("Reply received: " + reply);
        if (string.IsNullOrEmpty(reply))
        {
            replyText.text = "Empty reply!";
            return;
        }
        replyText.text = reply;
    }

    public void HideChatUI()
    {
        Player.playerInstance.isTalking = false;
        replyText.text = "HI !";
    }
}