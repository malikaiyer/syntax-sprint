
//New attempt with timer:
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] bugs;

    [SerializeField] private GameObject[] inactiveBugs;
    private int nextBugIndex = 0;
    public RobotController robot;
    public TokenSpawner tokenSpawner;

    public int score;
    public int lives;

    [SerializeField] private TextMeshProUGUI timerText;
    private float roundTimer;
    private bool isTimerRunning = false;

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text roundCompleteScreen;

    [SerializeField] private Image[] heartImages;

    [SerializeField] private GameObject worldIntroPanel;
    [SerializeField] private Text worldNameText;
    [SerializeField] private Button goToWorldButton;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource correctTokenSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource worldStartSound;
    [SerializeField] private AudioSource loseHeartSound;

    [SerializeField] private string[] worldNames = new string[] { "Python World", "Java Jungle", "C++ Circuit" };
    private int currentWorldIndex = 0;
    private int currentRound = 0;

    private void Start()
    {
        ShowWorldIntro();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }

        if (roundCompleteScreen.enabled && Input.anyKeyDown)
        {
            Time.timeScale = 1f;
            NewRound();
        }

        if (isTimerRunning)
        {
            roundTimer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(roundTimer / 60f);
            int seconds = Mathf.FloorToInt(roundTimer % 60f);
            timerText.text = $"{minutes:0}:{seconds:00}";

            if (roundTimer <= 10f)
            {
                FlashTimerColor();
            }
            else
            {
                timerText.color = Color.white;
            }

            if (roundTimer <= 0)
            {
                TimerEnded();
            }
        }
    }

    private void FlashTimerColor()
    {
        float t = Mathf.PingPong(Time.time * 5f, 1f);
        timerText.color = Color.Lerp(Color.white, Color.red, t);
    }

    private void TimerEnded()
    {
        isTimerRunning = false;
        Debug.Log("Timer ended — lost a heart!");
        SetLives(lives - 1);

        if (lives > 0)
        {
            loseHeartSound.Play();
            Invoke(nameof(ResetStateAndRestartTimer), 1.0f);
        }
        else
        {
            GameOver();
        }
    }

    private void ResetStateAndRestartTimer()
    {
        ResetState();
        StartTimerForCurrentRound(); // restart same round’s timer
    }

    private void NewGame()
    {
        if (gameOverSound.isPlaying)
        {
            gameOverSound.Stop();
        }

        currentRound = 0;
        SetScore(0);
        SetLives(3);
        tokenSpawner.ResetQuestions();
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;
        roundCompleteScreen.enabled = false;

        tokenSpawner.ClearTokens();
        tokenSpawner.SpawnTokens();
        ResetState();

        StartTimerForCurrentRound();
    }

    private void StartTimerForCurrentRound()
    {
        if (currentRound == 0)
            roundTimer = 60f;
        else if (currentRound == 1)
            roundTimer = 45f;
        else if (currentRound == 2)
            roundTimer = 30f;
        else
            roundTimer = 15f;

        isTimerRunning = true;
    }

    public void NewWorld()
    {
        Time.timeScale = 1f;
        worldIntroPanel.SetActive(false);
        tokenSpawner.ResetQuestions();
        tokenSpawner.ClearTokens();

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        NewRound();
    }

    private void ResetState()
    {
        foreach (GameObject bug in bugs)
        {
            bug.GetComponent<Bug>().ResetState();
        }

        robot.ResetState();

        foreach (GameObject inactive in inactiveBugs)
        {
            inactive.SetActive(false);
        }

        nextBugIndex = 0;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        foreach (GameObject bug in bugs)
        {
            bug.SetActive(false);
        }

        robot.gameObject.SetActive(false);

        foreach (GameObject inactive in inactiveBugs)
        {
            inactive.SetActive(false);
        }

        tokenSpawner.ClearTokens();

        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        gameOverSound.Play();
    }

    public void RoundComplete()
    {
        currentRound++;
        SetScore(score + 1);

        if (currentRound >= 5)
        {
            Debug.Log("World complete!");
            tokenSpawner.ClearTokens();

            currentWorldIndex++;
            if (currentWorldIndex >= worldNames.Length)
            {
                Debug.Log("All worlds complete! Game over.");
                GameOver();
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

    private void SetScore(int newScore)
    {
        score = newScore;
    }

    private void SetLives(int newLives)
    {
        lives = newLives;

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
        string worldName = worldNames[currentWorldIndex];
        worldNameText.text = $"Welcome to {worldName}!";
        worldIntroPanel.SetActive(true);
        goToWorldButton.onClick.RemoveAllListeners();
        goToWorldButton.onClick.AddListener(NewWorld);

        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        worldStartSound.Play();
    }

    public void PlayCorrectTokenSound()
    {
        correctTokenSound.Play();
    }

    public void ActivateNextBug()
    {
        if (nextBugIndex < inactiveBugs.Length)
        {
            GameObject bugToActivate = inactiveBugs[nextBugIndex];
            bugToActivate.SetActive(true);

            Bug bugScript = bugToActivate.GetComponent<Bug>();
            if (bugScript != null)
            {
                bugScript.ResetState();
            }

            nextBugIndex++;
        }
        else
        {
            Debug.Log("All bugs have already been activated.");
        }
    }
}

