using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Require a SpriteRenderer component
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } // Reference to the SpriteRenderer component
    public Sprite[] sprites; // Array of sprites for animation
    public float animationTime = 0.25f;
    public int animationFrame {get; private set; } // Current frame of the animation
    public bool loop = true; // Whether to loop the animation or not

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime); // Start the animation loop
    }

    private void Advance(){
        
        if(!this.spriteRenderer.enabled){
            return;
        }

        this.animationFrame++; // Increment the animation frame
        if(this.animationFrame >= this.sprites.Length && this.loop) // Check if the animation has reached the end
        {
            
                this.animationFrame = 0;
        }
         if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) // Check if the animation frame is valid
            {
                this.spriteRenderer.sprite = this.sprites[this.animationFrame]; // Set the current sprite
            }
        }
    
    public void Restart(){
        this.animationFrame = -1;
        Advance(); // Restart the animation
    }
    }

