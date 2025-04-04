using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BugController : MonoBehaviour
{
    public float speed = 3.0f;
    public LayerMask obstacleLayer;
    public float changeDirectionInterval = 2.0f; // Time before changing direction
    public float mapWidth = 10f; // Set according to your map size
    public float mapHeight = 10f;

    public Sprite bugUp;
    public Sprite bugDown;
    public Sprite bugLeft;
    public Sprite bugRight;

    private Rigidbody2D rb2D;
    private Vector2 direction;
    private float nextChangeTime;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.direction = GetRandomDirection();
    }

    private void Start()
    {
        this.transform.position = GetRandomEdgePosition();
        this.nextChangeTime = Time.time + changeDirectionInterval;
        UpdateSprite();
    }

    private void FixedUpdate()
    {
        Move();

        if (Time.time >= nextChangeTime)
        {
            ChangeDirection();
            nextChangeTime = Time.time + changeDirectionInterval;
            UpdateSprite();
        }
    }

    private void Move()
    {
        Vector2 position = this.rb2D.position;
        Vector2 translation = this.direction * this.speed * Time.fixedDeltaTime;
        
        if (!Occupied(direction))
        {
            this.rb2D.MovePosition(position + translation);
        }
        else
        {
            HandleCornerTrap();
        }
    }

    private void ChangeDirection()
    {
        this.direction = GetRandomDirection();
    }

    private Vector2 GetRandomDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        return directions[Random.Range(0, directions.Length)];
    }

    private bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

    private void HandleCornerTrap()
    {
        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 newDirection in possibleDirections)
        {
            if (!Occupied(newDirection))
            {
                direction = newDirection;
                UpdateSprite();
                return;
            }
        }
    }

    private Vector2 GetRandomEdgePosition()
    {
        float x, y;
        if (Random.value > 0.5f)
        {
            x = Random.Range(-mapWidth / 2, mapWidth / 2);
            y = (Random.value > 0.5f) ? mapHeight / 2 : -mapHeight / 2;
        }
        else
        {
            y = Random.Range(-mapHeight / 2, mapHeight / 2);
            x = (Random.value > 0.5f) ? mapWidth / 2 : -mapWidth / 2;
        }
        return new Vector2(x, y);
    }

    private void UpdateSprite()
    {
        if (direction == Vector2.up)
        {
            spriteRenderer.sprite = bugUp;
        }
        else if (direction == Vector2.down)
        {
            spriteRenderer.sprite = bugDown;
        }
        else if (direction == Vector2.left)
        {
            spriteRenderer.sprite = bugLeft;
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = bugRight;
        }
    }
}
