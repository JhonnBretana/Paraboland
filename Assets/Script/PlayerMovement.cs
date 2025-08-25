using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 🔹 Singleton instance
    public static PlayerMovement Instance;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    // Set your default spawn position for this map in the Inspector
    public Vector3 defaultSpawnPosition = new Vector3(2f, 0, 0);

    private Vector2 movement = Vector2.zero;

    // Flags for button states
    private bool upPressed, downPressed, leftPressed, rightPressed;

    void Awake()
    {
        // Ensure only one active instance exists
        Instance = this;
    }

    void Start()
    {
        upPressed = downPressed = leftPressed = rightPressed = false;

        Vector3 spawnPos = defaultSpawnPosition;

        if (PlayerPrefs.HasKey("PlayerSpawnX") && PlayerPrefs.HasKey("PlayerSpawnY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerSpawnX");
            float y = PlayerPrefs.GetFloat("PlayerSpawnY");
            spawnPos = new Vector3(x, y, defaultSpawnPosition.z);

            spawnPos += new Vector3(2f, 0, 0);

            PlayerPrefs.DeleteKey("PlayerSpawnX");
            PlayerPrefs.DeleteKey("PlayerSpawnY");
        }

        transform.position = spawnPos;
    }

    void Update()
    {
        movement = Vector2.zero;

        if (upPressed) movement.y += 1;
        if (downPressed) movement.y -= 1;
        if (leftPressed) movement.x -= 1;
        if (rightPressed) movement.x += 1;

        movement = movement.normalized;

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
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
        Vector3 exitPosition = transform.position;
        PlayerPrefs.SetFloat("PlayerSpawnX", exitPosition.x);
        PlayerPrefs.SetFloat("PlayerSpawnY", exitPosition.y);
    }
}
