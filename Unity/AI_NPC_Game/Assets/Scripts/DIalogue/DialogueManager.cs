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
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        replyText.text = "Thinking...";
        backend.SendMessage(message, OnReply);
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
    }
}