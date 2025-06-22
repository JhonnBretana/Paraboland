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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            // Get input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement
            if (input.x != 0) input.y = 0;

            // If there is input, move the player
            if (input != Vector2.zero)
            {
                // Set the animator parameters for idle
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = rb.position + input;
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
        // Temporarily disable our own collider so we don't hit ourselves
        boxCollider.enabled = false;
        // Check for colliders at the target position using a slightly smaller box
        Collider2D hit = Physics2D.OverlapBox((Vector2)targetPos + boxCollider.offset, boxCollider.size * 0.4f, 0.4f);
        // Re-enable our collider
        boxCollider.enabled = true;
        
        // If we didn't hit anything, the path is clear
        return hit == null;
    }
}
