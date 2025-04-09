using UnityEngine;
using System.Linq;

public class TokenSpawner : MonoBehaviour
{
    public GameObject tokenPrefab;
    public string[] correctSnippets;
    public string[] wrongSnippets;

    private Transform[] spawnPoints;

    private void Awake()
    {
        
        spawnPoints = GetComponentsInChildren<Transform>()
                      .Where(t => t != this.transform).ToArray();
    }

    private void Start()
    {
        SpawnTokens();
    }

    public void SpawnTokens()
    {

        Shuffle(spawnPoints);

        SpawnToken(spawnPoints[0].position, "correct_code_here", true);
        SpawnToken(spawnPoints[1].position, "wrong_code_here", false);
        SpawnToken(spawnPoints[2].position, "wrong_code_here", false);
    }

    void SpawnToken(Vector3 pos, string code, bool isCorrect)
    {
        GameObject token = Instantiate(tokenPrefab, pos, Quaternion.identity);
        token.GetComponent<Token>().Initialize(code, isCorrect);
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
}