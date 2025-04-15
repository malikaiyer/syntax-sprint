using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Require a Rigidbody2D component
public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidBody{get; private set;} // Reference to the Rigidbody2D component
    public float speed = 8.0f; // Speed of the object
    public float speedMultiplier = 1.0f; // Speed multiplier for the object
    public Vector2 initialDirection; // Initial direction of the object
    public LayerMask obstacleLayer; // Layer mask for obstacles

    public Vector2 direction {get; private set;} // Current direction of the object
    public Vector2 nextDirection {get; private set;} // Next direction of the object
    public Vector3 startingPosition; // Starting position of the object
    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        this.startingPosition = this.transform.position; // Set the starting position
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.nextDirection != Vector2.zero) // Check if there is a next direction
        {
            SetDirection(this.nextDirection); // Set the direction to the next direction
        }
    }

    public void Reset()
    {
        this.speedMultiplier = 1.0f; // Reset the speed multiplier to 1.0
        this.direction = this.initialDirection; // Set the direction to the initial direction
        this.nextDirection = Vector2.zero; // Set the next direction to zero
        this.transform.position = this.startingPosition; // Set the position to the starting position
        this.rigidBody.bodyType = RigidbodyType2D.Dynamic; // Set the body type to dynamic
        this.enabled = true; // Enable the script
    }

    private void FixedUpdate()
    {
        this.rigidBody.MovePosition(this.rigidBody.position + this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime); // Move the object in the current direction
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
       if (forced || !Occupied(direction)) // Check if the direction is occupied
        {
            // Debug.Log("Setting direction to: " + direction); // Log the new direction
            this.direction = direction; // Set the direction to the new direction
            this.nextDirection = Vector2.zero; // Reset the next direction
        }
        else{
            // Debug.Log("Direction blocked. setting next direction to" + direction); // Log the next direction
            this.nextDirection = direction; // Set the next direction to the new direction
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one*0.4f, 0.0f, direction, 1.0f, this.obstacleLayer); // Cast a ray to check for obstacles
        return hit.collider != null; 
    }


}
