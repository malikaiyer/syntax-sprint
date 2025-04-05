using UnityEngine;

public class BugScatter : BugBehavior
{
    private void OnDisable()
    {
        this.bug.chase.Enable(); // Enable the chase behavior when scatter is disabled
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            if (node.availableDirections.Count == 0)
        {
            Debug.LogWarning("Node has no available directions!");
            return;
        }
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index]== -this.bug.movement.direction && node.availableDirections.Count > 1)
            {
                // If the bug is already moving in the opposite direction, choose a different direction
                index = (index + 1) % node.availableDirections.Count;
            }
           
            this.bug.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
