using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    public Movement movement { get; private set; }
    public BugHome home { get; private set; }
    public BugScatter scatter { get; private set; }
    public BugChase chase { get; private set; }
    public BugBehavior initialBehavior;
    public Transform target;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<BugHome>();
        this.scatter = GetComponent<BugScatter>();
        this.chase = GetComponent<BugChase>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.Reset();
        this.chase.Disable();
        this.scatter.Enable();
        this.home.Disable();

        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Robot"))
        {
            FindFirstObjectByType<GameManager>().RobotEaten();

        }
    }
}
