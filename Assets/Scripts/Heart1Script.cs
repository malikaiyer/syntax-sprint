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




//New Attempt: 
