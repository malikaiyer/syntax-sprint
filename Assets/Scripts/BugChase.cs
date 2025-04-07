using UnityEngine;

public class BugChase : BugBehavior
{
    private void OnDisable()
    {
        this.bug.scatter.Enable(); // Enable the chase behavior when scatter is disabled
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && this.enabled)
        {
            //find the shortest path to the target (robot)
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.bug.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;

                }
            }
            this.bug.movement.SetDirection(direction);
        }
    }
}
