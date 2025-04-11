using UnityEngine;

public class Heart1Script : MonoBehaviour
{
    public int totalLives = 3;
    public GameObject[] heartImages;

    private int currentLives;

    public int GetCurrentLives()
    {
        return currentLives;
    }

    private void Start()
    {
        currentLives = totalLives;
        UpdateHearts();
    }

    public void LoseLife()
    {
        currentLives--;

        if (currentLives < 0)
            return;

        UpdateHearts();

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            // Do not reset robot here. GameManager will check life count and restart if needed.
        }
    }

    public void ResetHearts()
    {
        currentLives = totalLives;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].SetActive(i < currentLives);
        }
    }
}


// // New attempt with game over text
// using UnityEngine;
// using UnityEngine.UI;  // Add this for accessing the UI Text element

// public class Heart1Script : MonoBehaviour
// {
//     public int totalLives = 3;
//     public GameObject[] heartImages;
//     public Text gameOverText;  // Reference to the "GAME OVER" Text UI element

//     private int currentLives;

//     public int GetCurrentLives()
//     {
//         return currentLives;
//     }

//     private void Start()
//     {
//         currentLives = totalLives;
//         UpdateHearts();
//         gameOverText.gameObject.SetActive(false);  // Ensure GameOver text is hidden initially
//     }

//     public void LoseLife()
//     {
//         currentLives--;

//         if (currentLives < 0)
//             return;

//         UpdateHearts();

//         if (currentLives <= 0)
//         {
//             Debug.Log("Game Over!");
//             // Show the "GAME OVER" text when lives reach 0
//             gameOverText.gameObject.SetActive(true);
//         }
//     }

//     public void ResetHearts()
//     {
//         currentLives = totalLives;
//         UpdateHearts();
//         gameOverText.gameObject.SetActive(false);  // Hide the "GAME OVER" text when resetting lives
//     }

//     private void UpdateHearts()
//     {
//         for (int i = 0; i < heartImages.Length; i++)
//         {
//             heartImages[i].SetActive(i < currentLives);
//         }
//     }
// }
