using UnityEngine;
using System.Linq;
using System.Collections.Generic;


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

    private void Awake()
    {
        
        spawnPoints = GetComponentsInChildren<Transform>()
                      .Where(t => t != this.transform).ToArray();
    }

    private void Start()
    {
        // SpawnTokens();
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
    Debug.Log("Current question: " + question.questionText);
    currentQuestionIndex++;

    // Create a list of token data with one correct and two wrong answers
    List<(string, bool)> tokens = new List<(string, bool)>
    {
        (question.correctAnswer, true),
        (question.wrongAnswers[0], false),
        (question.wrongAnswers[1], false)
    };

    // Shuffle the token list so the correct answer isn't always first
    tokens = tokens.OrderBy(x => Random.value).ToList();

    // Spawn tokens at the shuffled spawn points
    for (int i = 0; i < 3; i++)
    {
        SpawnToken(spawnPoints[i].position, tokens[i].Item1, tokens[i].Item2);
    }
    }

    void SpawnToken(Vector3 pos, string code, bool isCorrect)
    {
        GameObject token = Instantiate(tokenPrefab, pos, Quaternion.identity);
        token.GetComponent<Token>().Initialize(code, isCorrect, gameManager);
        activeTokens.Add(token); // Track the token

    }

    void Shuffle(Transform[] points)
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

    public void ResetQuestions()
    {
        currentQuestionIndex = 0;
    }


}