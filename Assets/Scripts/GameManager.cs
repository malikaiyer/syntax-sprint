using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject[] bugs;
    [SerializeField] private GameObject[] inactiveBugs;
    private int nextBugIndex = 0;

    public RobotController robot;
    public TokenSpawner tokenSpawner;

    public int score;
    public int lives = 3;

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text roundCompleteScreen;
    [SerializeField] private Image[] heartImages;

    [SerializeField] private GameObject worldIntroPanel;
    [SerializeField] private Text worldNameText;
    [SerializeField] private Button goToWorldButton;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource correctTokenSound;
    [SerializeField] private AudioSource wrongTokenSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource worldStartSound;
    [SerializeField] private AudioSource loseHeartSound;

    [SerializeField] private TextMeshProUGUI readingCountdownText;

    [SerializeField] private string[] worldNames = new string[] { "Python World", "Java Jungle", "C++ Circuit" };
    private int currentWorldIndex = 0;
    private int currentRound = 0;

    private float readingTime = 5f;

    private void Start()
    {
        ShowWorldIntro();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }

        if (roundCompleteScreen.enabled && Input.anyKeyDown)
        {
            Time.timeScale = 1f;
            NewRound();
        }
    }

    private void NewGame()
    {
        if (gameOverSound.isPlaying)
            gameOverSound.Stop();

        tokenSpawner.ResetQuestions();
        currentWorldIndex = 0;
        currentRound = 0;
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;
        roundCompleteScreen.enabled = false;
        tokenSpawner.ClearTokens();
        ResetState();
        StartCoroutine(StartReadingCountdown());
    }

    private IEnumerator StartReadingCountdown()
    {
        Time.timeScale = 0f;
        float timeLeft = readingTime;
        readingCountdownText.gameObject.SetActive(true);

        tokenSpawner.SpawnTokens(); // show question and spawn tokens early

        var pulse = readingCountdownText.GetComponent<PulseText>();

        while (timeLeft > 0)
        {
            readingCountdownText.text = Mathf.Ceil(timeLeft).ToString();
            if (pulse != null) pulse.PlayPulse();
            yield return new WaitForSecondsRealtime(1f);
            timeLeft -= 1f;
        }

        readingCountdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NewWorld()
    {
        Time.timeScale = 1f;
        worldIntroPanel.SetActive(false);
        tokenSpawner.ClearTokens();

        if (!backgroundMusic.isPlaying)
            backgroundMusic.Play();

        NewRound();
    }

    private void ResetState()
    {
        foreach (var bug in bugs)
            bug.GetComponent<Bug>().ResetState();

        robot.ResetState();

        foreach (var bug in inactiveBugs)
            bug.SetActive(false);

        nextBugIndex = 0;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        foreach (var bug in bugs)
            bug.SetActive(false);

        robot.gameObject.SetActive(false);

        foreach (var bug in inactiveBugs)
            bug.SetActive(false);

        tokenSpawner.ClearTokens();

        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();

        gameOverSound.Play();
    }

    private void GameWon()
    {
        gameOverText.enabled = true;
        gameOverText.text = "Congratulations! All worlds complete!";

        foreach (var bug in bugs)
            bug.SetActive(false);

        robot.gameObject.SetActive(false);

        foreach (var bug in inactiveBugs)
            bug.SetActive(false);

        tokenSpawner.ClearTokens();

        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }

    public void RoundComplete()
    {
        currentRound++;
        SetScore(score + 1);
        tokenSpawner.NextQuestion();

        if (currentRound >= 5)
        {
            currentWorldIndex++;
            if (currentWorldIndex >= worldNames.Length)
            {
                GameWon();
            }
            else
            {
                currentRound = 0;
                SetLives(3);
                ShowWorldIntro();
            }
        }
        else
        {
            tokenSpawner.ClearTokens();
            roundCompleteScreen.enabled = true;
            Time.timeScale = 0f;
        }
    }

    private void SetScore(int value)
    {
        score = value;
    }

    private void SetLives(int value)
    {
        lives = value;
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < lives;
        }
    }

    public void RobotEaten()
    {
        robot.gameObject.SetActive(false);
        SetLives(lives - 1);

        if (lives > 0)
        {
            loseHeartSound.Play();
            Invoke(nameof(ResetState), 1.0f);
        }
        else
        {
            GameOver();
        }
    }

    private void ShowWorldIntro()
    {
        Time.timeScale = 0f;
        worldNameText.text = $"Welcome to {worldNames[currentWorldIndex]}!";
        worldIntroPanel.SetActive(true);
        goToWorldButton.onClick.RemoveAllListeners();
        goToWorldButton.onClick.AddListener(NewWorld);

        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();

        worldStartSound.Play();
    }

    public void PlayCorrectTokenSound() => correctTokenSound.Play();
    public void PlayWrongTokenSound() => wrongTokenSound.Play();

    public void ActivateNextBug()
    {
        if (nextBugIndex < inactiveBugs.Length)
        {
            GameObject bugToActivate = inactiveBugs[nextBugIndex];
            bugToActivate.SetActive(true);

            Bug bugScript = bugToActivate.GetComponent<Bug>();
            bugScript?.ResetState();

            nextBugIndex++;
        }
    }
}
