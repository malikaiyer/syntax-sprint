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
            Debug.Log("Correct token collected!");
            gameManager.RoundComplete(); //trigger the next question
            
        }
        else
        {
            Debug.Log("Wrong token collected!");
            
        }

        Destroy(gameObject); 
    }
}

}