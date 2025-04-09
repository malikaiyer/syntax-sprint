using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    public GameObject[] bugs;
    public RobotController robot;


    public int score; 
    public int lives; 
    [SerializeField] private Text gameOverText;


    private void Start()
    {
        NewGame();

    }

    private void Update()
    {
        if(this.lives <= 0 && Input.anyKeyDown)
        {NewGame();}
    }
    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound(){
        gameOverText.enabled = false;
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
    }
    private void SetScore(int score){
        this.score = score;
    
    }

    private void SetLives(int lives){
        this.lives = lives;
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
