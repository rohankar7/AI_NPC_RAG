using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float speed = 0.5f;
    public bool isTalking = false;
    public static Player playerInstance;
    public string npc_id = "";
    [SerializeField] private TMP_Text replyText;
    void Awake()
    {
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(gameObject);
        }
        playerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!isTalking)
        {
            Vector2 inputVector = Vector2.zero;
            if (Gamepad.current != null)
            {
                inputVector = Gamepad.current.leftStick.ReadValue();
            }
            else if (Keyboard.current != null)
            {
                float x = (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) ? 1 : (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) ? -1 : 0;
                float y = (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) ? 1 : (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) ? -1 : 0;
                inputVector = new Vector2(x, y);
            }
            transform.position += speed * Time.deltaTime * new Vector3(inputVector.x, inputVector.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            //Debug.Log("Ready to talk?");
            npc_id = collision.gameObject.name.ToLower();
            isTalking = true;
            switch (npc_id)
            {
                case "blacksmith": replyText.text = "Rowan, the blacksmith"; break;
                case "merchant": replyText.text = "Mira, the merchant"; break;
                case "guard": replyText.text = "Alden, the guard"; break;
            }
        }
    }
}
