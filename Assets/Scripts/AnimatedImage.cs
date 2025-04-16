using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))] // Require an Image component for UI
public class AnimatedUISprite : MonoBehaviour
{
    public Image image { get; private set; } // Reference to the UI Image component
    public Sprite[] sprites; // Array of sprites for animation
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; } // Current frame of the animation
    public bool loop = true; // Whether to loop the animation or not

    void Awake()
    {
        this.image = GetComponent<Image>();
    }

    void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime); // Start the animation loop
    }

    private void Advance()
    {
        if (!this.image.enabled)
        {
            return;
        }

        this.animationFrame++; // Increment the animation frame

        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0; // Loop back to the beginning
        }

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.image.sprite = this.sprites[this.animationFrame]; // Set the current sprite
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;
        Advance(); // Restart the animation
    }
}
