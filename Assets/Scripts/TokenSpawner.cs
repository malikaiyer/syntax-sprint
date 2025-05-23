// using UnityEngine;
// using System.Linq;
// using System.Collections.Generic;
// using UnityEngine.UI;



// public class TokenSpawner : MonoBehaviour
// {
//     public GameObject tokenPrefab;
//     public string[] correctSnippets;
//     public string[] wrongSnippets;

//     private Transform[] spawnPoints;
//     public List<QuestionsAndAnswers> quizQuestions;
//     private List<GameObject> activeTokens = new List<GameObject>();
//     private int currentQuestionIndex = 0;
//     public GameManager gameManager; // Reference to the GameManager
//     [SerializeField] private Text QuestionText;


//     private void Awake()
//     {

//         spawnPoints = GetComponentsInChildren<Transform>()
//                       .Where(t => t != this.transform).ToArray();
//     }

//     private void Start()
//     {
//         // SpawnTokens();
//     }

//     public void SpawnTokens()
//     {

//         Shuffle(spawnPoints);

//         // Get the current question in order
//     if (currentQuestionIndex >= quizQuestions.Count)
//     {
//         Debug.Log("No more questions left.");
//         return;
//     }

//     var question = quizQuestions[currentQuestionIndex];
//     QuestionText.text = question.questionText; // Update the UI text with the current question
//     Debug.Log("Current question: " + question.questionText);

//     // Create a list of token data with one correct and two wrong answers
//     List<(string, bool)> tokens = new List<(string, bool)>
//     {
//         (question.correctAnswer, true),
//         (question.wrongAnswers[0], false),
//         (question.wrongAnswers[1], false)
//     };

//     // Shuffle the token list so the correct answer isn't always first
//     tokens = tokens.OrderBy(x => Random.value).ToList();

//     // Spawn tokens at the shuffled spawn points
//     List<Transform> randomSpawnPoints = spawnPoints.OrderBy(x => Random.value).Take(3).ToList();

// // Spawn tokens at those 3 random spawn points
// for (int i = 0; i < 3; i++)
// {
//     SpawnToken(randomSpawnPoints[i].position, tokens[i].Item1, tokens[i].Item2);
// }}

//     void SpawnToken(Vector3 pos, string code, bool isCorrect)
//     {
//         GameObject token = Instantiate(tokenPrefab, pos, Quaternion.identity);
//         token.GetComponent<Token>().Initialize(code, isCorrect, gameManager);
//         activeTokens.Add(token); // Track the token

//     }

//     void Shuffle(Transform[] points)
//     {
//         for (int i = 0; i < points.Length; i++)
//         {
//             int rand = Random.Range(i, points.Length);
//             var temp = points[i];
//             points[i] = points[rand];
//             points[rand] = temp;
//         }
//     }

//     public void ClearTokens()
//     {
//         foreach (GameObject token in activeTokens)
//         {
//             if (token != null)
//             {
//                 Destroy(token);
//             }
//         }
//         activeTokens.Clear();
//     }

//     public void NextQuestion(){
//         currentQuestionIndex++;
//     }

//     public void ResetQuestions()
//     {
//         currentQuestionIndex = 0;
//     }


// }


//New Attempt with timer
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class TokenSpawner : MonoBehaviour
{
    public GameObject tokenPrefab;
    public string[] correctSnippets;
    public string[] wrongSnippets;

    private Transform[] spawnPoints;
    public List<QuestionsAndAnswers> quizQuestions;
    private List<GameObject> activeTokens = new List<GameObject>();
    private int currentQuestionIndex = 0;

    public GameManager gameManager; // Reference to the GameManager
    [SerializeField] private Text QuestionText; // UI Text to display the question

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>()
                      .Where(t => t != this.transform).ToArray();
    }

    private void Start()
    {
        // Tokens are spawned manually via GameManager after countdown
    }

    public void SpawnTokens()
    {
        Shuffle(spawnPoints);

        // Get the current question in order
        if (currentQuestionIndex >= quizQuestions.Count)
        {
            Debug.Log("No more questions left.");
            return;
        }

        var question = quizQuestions[currentQuestionIndex];

        // ✅ Show question text on screen
        if (QuestionText != null)
        {
            QuestionText.text = question.questionText;
        }

        Debug.Log("Current question: " + question.questionText);

        // Prepare token data: 1 correct + 2 wrong
        List<(string, bool)> tokens = new List<(string, bool)>
        {
            (question.correctAnswer, true),
            (question.wrongAnswers[0], false),
            (question.wrongAnswers[1], false)
        };

        // Shuffle token order and pick 3 random spawn points
        tokens = tokens.OrderBy(x => Random.value).ToList();
        List<Transform> randomSpawnPoints = spawnPoints.OrderBy(x => Random.value).Take(3).ToList();

        // Spawn 3 tokens
        for (int i = 0; i < 3; i++)
        {
            SpawnToken(randomSpawnPoints[i].position, tokens[i].Item1, tokens[i].Item2);
        }
    }

    private void SpawnToken(Vector3 pos, string code, bool isCorrect)
    {
        GameObject token = Instantiate(tokenPrefab, pos, Quaternion.identity);
        token.GetComponent<Token>().Initialize(code, isCorrect, gameManager);
        activeTokens.Add(token); // Track for clearing later
    }

    private void Shuffle(Transform[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            int rand = Random.Range(i, points.Length);
            var temp = points[i];
            points[i] = points[rand];
            points[rand] = temp;
        }
    }

    public void ClearTokens()
    {
        foreach (GameObject token in activeTokens)
        {
            if (token != null)
            {
                Destroy(token);
            }
        }
        activeTokens.Clear();
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
    }

    public void ResetQuestions()
    {
        currentQuestionIndex = 0;
    }
}
