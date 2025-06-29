using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public LayerMask obstacleLayer;
    public float checkRadius = 0.1f; // Adjustable radius for collision check
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = (Vector2)transform.position + input;
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

        while ((targetPos - (Vector2)transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsPathClear(Vector2 targetPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(targetPos, checkRadius, obstacleLayer);
        return hit == null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}