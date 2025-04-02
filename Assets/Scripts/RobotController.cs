using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public Sprite robotLeft; // Sprite for moving left
    public Sprite robotRight; // Sprite for moving right
    public Sprite robotIdle;

    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveRobot();
    }

    void MoveRobot()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 (left), 1 (right), 0 (idle)
        float moveY = Input.GetAxisRaw("Vertical");   // -1 (down), 1 (up), 0 (idle)

        moveDirection = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement

        if (moveX < 0)
        {
            spriteRenderer.sprite = robotLeft; // Change sprite when moving left
        }
        else if (moveX > 0)
        {
            spriteRenderer.sprite = robotRight; // Change sprite when moving right
        }
        else
        {
            spriteRenderer.sprite = robotIdle; // Reset sprite when idle
        }

        transform.position += (Vector3)moveDirection * speed * Time.deltaTime; // Move the robot
    }
}
