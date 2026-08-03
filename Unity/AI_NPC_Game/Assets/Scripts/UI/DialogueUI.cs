using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public RectTransform panel;
    private string playerMessage;
    [SerializeField] public Button sendButton;

    void Start()
    {
        UpdateButtonState(inputField.text);
        inputField.onValueChanged.AddListener(UpdateButtonState);
    }
    void Update()
    {
        if (Player.playerInstance.isTalking)
        {
            panel.gameObject.SetActive(true);
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }
    private void UpdateButtonState(string text)
    {
        sendButton.interactable = !string.IsNullOrWhiteSpace(text);
    }

    public void SendPlayerMessage()
    {
        playerMessage = inputField.text;
        Debug.Log(playerMessage);
        inputField.text = string.Empty;
        sendButton.interactable = false;
    }

    private void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(UpdateButtonState);
        }
    }
}