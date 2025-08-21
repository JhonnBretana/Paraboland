using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    // Set your default spawn position for this map in the Inspector
    public Vector3 defaultSpawnPosition = new Vector3(0, 0, 0);

    private Vector2 movement = Vector2.zero;

    // Flags for button states
    private bool upPressed, downPressed, leftPressed, rightPressed;

    void Start()
    {
        upPressed = downPressed = leftPressed = rightPressed = false;

        Vector3 spawnPos = defaultSpawnPosition; // Always use default unless returning

        // Check if returning from a house/battle
        if (PlayerPrefs.HasKey("PlayerSpawnX") && PlayerPrefs.HasKey("PlayerSpawnY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerSpawnX");
            float y = PlayerPrefs.GetFloat("PlayerSpawnY");
            spawnPos = new Vector3(x, y, defaultSpawnPosition.z);

            // Add offset to avoid trigger collider
            spawnPos += new Vector3(0.5f, 0, 0);

            PlayerPrefs.DeleteKey("PlayerSpawnX");
            PlayerPrefs.DeleteKey("PlayerSpawnY");
        }

        transform.position = spawnPos;
    }

    void Update()
    {
        // Debug to check if a button is stuck
        Debug.Log($"UP:{upPressed} DOWN:{downPressed} LEFT:{leftPressed} RIGHT:{rightPressed}");

        // Reset movement every frame
        movement = Vector2.zero;

        if (upPressed) movement.y += 1;
        if (downPressed) movement.y -= 1;
        if (leftPressed) movement.x -= 1;
        if (rightPressed) movement.x += 1;

        // Normalize to prevent faster diagonal movement
        movement = movement.normalized;

        // Animate based on direction
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        // Move the player smoothly
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // 🔻 Button handlers

    public void PressUp() => upPressed = true;
    public void ReleaseUp() => upPressed = false;

    public void PressDown() => downPressed = true;
    public void ReleaseDown() => downPressed = false;

    public void PressLeft() => leftPressed = true;
    public void ReleaseLeft() => leftPressed = false;

    public void PressRight() => rightPressed = true;
    public void ReleaseRight() => rightPressed = false;

    public void SaveSpawnPoint()
    {
        // Save current position with offset to avoid collider
        Vector3 exitPosition = transform.position + new Vector3(0.5f, 0, 0); // Adjust offset as needed
        PlayerPrefs.SetFloat("PlayerSpawnX", exitPosition.x);
        PlayerPrefs.SetFloat("PlayerSpawnY", exitPosition.y);
    }
}
