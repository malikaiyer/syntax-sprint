using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotController : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody; 

    public Vector2 direction; 

    public Vector2 nextDirection;
    public Vector3 startingPosition; 
    public Sprite robotLeft;
    public Sprite robotRight;
    public Sprite robotIdle;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
        this.spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component

    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        this.enabled = true; 
        UpdateSprite(); // Update sprite to initial direction
    }

    private void Update()
    {
        if(nextDirection != Vector2.zero){
            SetDirection(this.nextDirection);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SetDirection(Vector2.right);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
            UpdateSprite(); // Update sprite to new direction
        }
        else{
            this.nextDirection = direction;
        }
    }

   public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null; 
    } 

     private void UpdateSprite()
    {
        if (direction == Vector2.left)
        {
            spriteRenderer.sprite = robotLeft;
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = robotRight;
        }
        else
        {
            spriteRenderer.sprite = robotIdle;
        }
    }


}
