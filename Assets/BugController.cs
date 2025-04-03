using UnityEngine;

public class BugController : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the bug
    private Vector2 targetDirection;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Sprite[] bugSprites; // Assign different bug sprites in Unity Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Assign a random sprite
        AssignRandomSprite();

        // Spawn the bug at a random corner
        transform.position = GetRandomCornerPosition();

        // Pick an initial random direction
        ChangeDirection();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = targetDirection * speed;
    }

    void ChangeDirection()
    {
        // Pick a random direction
        targetDirection = Random.insideUnitCircle.normalized;
    }

    Vector3 GetRandomCornerPosition()
    {
        float x = Random.Range(0, 2) == 0 ? -8f : 8f;  // Example values for corners
        float y = Random.Range(0, 2) == 0 ? -4f : 4f;
        return new Vector3(x, y, 0);
    }

    void AssignRandomSprite()
    {
        if (bugSprites.Length > 0)
        {
            spriteRenderer.sprite = bugSprites[Random.Range(0, bugSprites.Length)];
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Change direction when colliding with an obstacle
        ChangeDirection();
    }
}
