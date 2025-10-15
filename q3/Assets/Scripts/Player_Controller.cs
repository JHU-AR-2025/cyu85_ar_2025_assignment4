using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;

    public float speed = 1f;
    public float jumpForce = 0f;

    private int numCollected = 0;
    public int total = 0;

    public GameObject youWinObject;
    public GameObject SpeedUpObject;
    public TextMeshProUGUI counterText;

    public GameObject startButton;      // assign StartButton here
    public GameObject blockerPanel;     // optional: full-screen panel

    private bool isActive;
    private bool isPowerUp;

    public GameObject capsule;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();

        if (youWinObject) youWinObject.SetActive(false);
        if (counterText) counterText.text = numCollected + "/" + total;

        isActive = false;
        rb.useGravity = false;

        if (capsule) capsule.SetActive(false);
        if (SpeedUpObject) SpeedUpObject.SetActive(false);
        isPowerUp = false;

        // disable input before start
        moveAction?.Disable();
        jumpAction?.Disable();

        if (blockerPanel) blockerPanel.SetActive(true);
    }

    public void StartButton()
    {
        isActive = true;
        rb.useGravity = true;

        moveAction?.Enable();
        jumpAction?.Enable();

        if (startButton) startButton.SetActive(false);
        if (blockerPanel) blockerPanel.SetActive(false);

        EventSystem.current?.SetSelectedGameObject(null);
        Debug.Log("Game Started!");
    }

    private void FixedUpdate()
    {
        if (!isActive) return;

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0f, moveValue.y);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (!isActive) return;

        if (jumpAction.WasPressedThisFrame())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            numCollected++;
            if (counterText) counterText.text = numCollected + "/" + total;
            if (numCollected >= total && capsule) capsule.SetActive(true);
        }
        else if (other.CompareTag("Capsule"))
        {
            other.gameObject.SetActive(false);
            speed *= 2f;
            var r = GetComponent<Renderer>();
            if (r) r.material.color = Color.blue;
            if (SpeedUpObject) SpeedUpObject.SetActive(true);
            isPowerUp = true;
        }
        else if (other.CompareTag("Goal"))
        {
            other.gameObject.SetActive(false);
            if (youWinObject) youWinObject.SetActive(true);
            Debug.Log("You win!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            if (isPowerUp)
            {
                var wall = other.gameObject.GetComponent<BreakableWall_Controller>();
                if (wall != null) wall.Break();
                Debug.Log("Break the wall!");
            }
            else
            {
                Debug.Log("Hit wall, but no power-up");
            }
        }
    }
}