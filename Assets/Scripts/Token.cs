using UnityEngine;

public class Token : MonoBehaviour
{
    public string codeText;
    public bool isCorrect;

    public void Initialize(string code, bool correct)
    {
        codeText = code;
        isCorrect = correct;
        
    }

      private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player")) 
    {
        if (isCorrect)
        {
            Debug.Log("Correct token collected!");
            
        }
        else
        {
            Debug.Log("Wrong token collected!");
            
        }

        Destroy(gameObject); 
    }
}

}