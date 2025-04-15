using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsAndAnswers", menuName = "Quiz/QuestionsAndAnswers")]
public class QuestionsAndAnswers : ScriptableObject
{
    [TextArea]
    public string questionText;

    public string correctAnswer;
    public string[] wrongAnswers;
}
