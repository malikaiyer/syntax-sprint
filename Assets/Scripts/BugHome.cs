using System.Collections;
using UnityEngine;

public class BugHome : BugBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {
        this.bug.movement.SetDirection(Vector2.up, true);
        this.bug.movement.rigidBody.bodyType = RigidbodyType2D.Kinematic;
        this.bug.movement.enabled = false;

        //animating the position of the bug 
        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.bug.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f; 

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.bug.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.bug.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f: 1.0f, 0.0f), true);
        this.bug.movement.rigidBody.bodyType = RigidbodyType2D.Dynamic;
        this.bug.movement.enabled = true;
    }
}
