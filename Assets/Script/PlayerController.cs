using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Collision Settings")]
    public LayerMask solidObjectsLayer; // ← assign this in the Inspector (e.g., to Walls)

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (rb != null)
        {
            rb.gravityScale = 0; // Top-down movement, so no gravity
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            // Get input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                // Set animator parameters
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                // Calculate target position
                Vector2 targetPos = rb.position + input;

                if (IsPathClear(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;

        while ((targetPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }

        rb.MovePosition(targetPos);
        isMoving = false;
    }

    private bool IsPathClear(Vector3 targetPos)
    {
        boxCollider.enabled = false;

        // Adjust the check box size and position
        Collider2D hit = Physics2D.OverlapBox(
            (Vector2)targetPos + boxCollider.offset,
            boxCollider.size * 0.4f,
            0f,
            solidObjectsLayer // Only check against walls
        );

        boxCollider.enabled = true;
        return hit == null;
    }

    // Debug visual for the collision check box
    void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.red;

        Vector2 checkPosition = (Vector2)transform.position + boxCollider.offset + input;
        Gizmos.DrawWireCube(checkPosition, boxCollider.size * 0.4f);
    }
}
