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

    //for world screen
    [SerializeField] private GameObject worldIntroPanel;
    [SerializeField] private Text worldNameText;
    [SerializeField] private Button goToWorldButton;
    [SerializeField] private string[] worldNames = new string[] { "Python World", "Java Jungle", "C++ Circuit" };
    private int currentWorldIndex = 0;

    private int currentRound = 0;

    private void Start()
    {
        ShowWorldIntro();
        // NewGame();

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
        tokenSpawner.ClearTokens();
        tokenSpawner.SpawnTokens();
        ResetState();
    }

    public void NewWorld(){
        Time.timeScale = 1f; // Resume time
        worldIntroPanel.SetActive(false);
        tokenSpawner.ResetQuestions();
        tokenSpawner.ClearTokens();
        NewRound();
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
            Debug.Log("World complete!");
            tokenSpawner.ClearTokens();
            
            //show world complete screen
            //set to new map
            //set hearts to full
            //keep score same
            //keep round score the same

             currentWorldIndex++;
            if (currentWorldIndex >= worldNames.Length)
            {
                Debug.Log("All worlds complete! Game over.");
                GameOver();
            }
            else
            {
                currentRound = 0;            // Reset round for next world
                SetLives(3);                 // Reset lives for new world
                ShowWorldIntro();           // Show next world screen
            }
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

    private void ShowWorldIntro()
    {
        Time.timeScale = 0f;
        string worldName = worldNames[currentWorldIndex];
        worldNameText.text = $"Welcome to the {worldName}!";
        worldIntroPanel.SetActive(true);
        goToWorldButton.onClick.RemoveAllListeners();
        goToWorldButton.onClick.AddListener(NewWorld);
    }


}
