using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private float speed = 0.5f;
    public bool isTalking = false;
    public static Player playerInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(gameObject);
        }
        playerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
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
            Debug.Log("Ready to talk?");
            isTalking = true;
        }
    }
}
