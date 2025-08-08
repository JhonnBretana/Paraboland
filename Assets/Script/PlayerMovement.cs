using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement = Vector2.zero;

    // Flags for button states
    private bool upPressed, downPressed, leftPressed, rightPressed;

    void Start()
    {
        // Make sure nothing is moving at the start
        upPressed = downPressed = leftPressed = rightPressed = false;
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
}
