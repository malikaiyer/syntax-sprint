using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Token : MonoBehaviour
{
    public string codeText;
    public bool isCorrect;
    public List<QuestionsAndAnswers> quizQuestions;
    public GameManager gameManager;

    private TextMeshProUGUI textDisplay;


     private void Awake()
    {
        // Find the TMP component in children (adjust name if needed)
        textDisplay = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(string code, bool correct, GameManager gm)
    {
        codeText = code;
        isCorrect = correct;
        gameManager = gm;

        if(textDisplay!=null){
            textDisplay.text = codeText; // Set the text to the TMP component
        }
        
    }

      private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player")) 
    {
        if (isCorrect)
        {
            gameManager.PlayCorrectTokenSound(); // Play sound for correct token
            Debug.Log("Correct token collected!");
            gameManager.RoundComplete(); //trigger the next question
            
        }
        else
        {
            gameManager.PlayWrongTokenSound(); // Play sound for wrong token
            // Assuming you have a reference to the RobotController instance
            RobotController robotController = FindFirstObjectByType<RobotController>();
            if (robotController != null)
            {
                robotController.updateRobotSpriteError();
            }
            else
            {
                Debug.LogError("RobotController instance not found!");
            }
            Debug.Log("Wrong token collected!");
            gameManager.ActivateNextBug();   // Activate a bug when wrong token is collected

            
        }

        Destroy(gameObject); 
    }
}

}