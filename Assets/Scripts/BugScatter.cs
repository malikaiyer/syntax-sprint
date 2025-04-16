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
            Debug.LogWarning(this.bug + " Node has no available directions!");
            return;
        }
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections.Count > 1 && node.availableDirections[index]== -bug.movement.direction)
            {
                // If the bug is already moving in the opposite direction, choose a different direction
                index = index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0; // Wrap around to the first direction
                }
            }
           
            this.bug.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
