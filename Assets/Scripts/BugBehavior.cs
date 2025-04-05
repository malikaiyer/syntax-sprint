using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Bug))]
public abstract class BugBehavior : MonoBehaviour
{
    public Bug bug{get; private set;}
    public float duration;

    public void Awake()
    {
        this.bug = GetComponent<Bug>();
        this.enabled = false; // Disable this script by default   
    }

    public virtual void Enable()
    {
        Enable(this.duration);
    }

    public void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration); 
    }

    public virtual void Disable()
    {
        this.enabled = false; // Disable this script
        CancelInvoke(); // Cancel any pending invocations of the Disable method
    }
}