using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    public GameObject[] bugs;
    public RobotController robot;
    public TokenSpawner tokenSpawner;


    public int score; 
    public int lives; 
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text roundCompleteScreen;

    [SerializeField] private Image[] heartImages; // UI hearts representing lives

    private int currentRound = 0;

    private void Start()
    {
        NewGame();

    }

    private void Update()
    {
        if(this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        
        }

        if (roundCompleteScreen.enabled && Input.anyKeyDown)
        {
            Time.timeScale = 1f;
            NewRound();
        }
    }

    //ONLY CALL IF YOU WANT TO START THE GAME FROM SCRATCH - called when game is over 
    private void NewGame(){
        currentRound = 0;
        SetScore(0);
        SetLives(3);
        tokenSpawner.ResetQuestions();
        NewRound();
    }

    //starts a new round
    private void NewRound(){
        gameOverText.enabled = false;
        roundCompleteScreen.enabled  = false;

        //spawn tokens
        tokenSpawner.SpawnTokens();
        ResetState();
    }

    private void ResetState(){
        for(int i = 0; i < this.bugs.Length; i++)
        {
            this.bugs[i].GetComponent<Bug>().ResetState();
        }
            this.robot.ResetState();
            
    }

    private void GameOver(){
        gameOverText.enabled = true;
        for(int i = 0; i < this.bugs.Length; i++)
        {
            this.bugs[i].gameObject.SetActive(false);
        }
             this.robot.gameObject.SetActive(false);

        // Clear tokens
        tokenSpawner.ClearTokens();
    }

    //this is called when a round is complete (the robot eats the correct token)
    public void RoundComplete(){
        currentRound++;
        SetScore(this.score + 1);
    if (currentRound >= 5)
        {
            // World complete logic here (e.g., switch to next language, reset hearts, etc.)
            Debug.Log("World complete!");
            // Possibly transition to another scene or reset question list
        }
    else
        {
            tokenSpawner.ClearTokens();
            roundCompleteScreen.enabled = true;
            Time.timeScale = 0f; // Pause the game
        }
    }

    private void SetScore(int score){
        this.score = score;
    
    }

    private void SetLives(int lives){
        this.lives = lives;

        for (int i = 0; i < heartImages.Length; i++){
            heartImages[i].enabled = i < lives;
        }
    }

    public void RobotEaten(){
        this.robot.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0){
            Invoke(nameof(ResetState), 1.0f);
        }
        else{
            GameOver();
        }
    }

}
