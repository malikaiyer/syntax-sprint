using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public GameObject[] bugs;
    public RobotController robot;


    public int score; 
    public int lives; 

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

        ResetState();
    }

    private void ResetState(){
                for(int i = 0; i < this.bugs.Length; i++)
        {
            this.bugs[i].gameObject.SetActive(true);
            }
             this.robot.gameObject.SetActive(true);
    }
    private void GameOver(){
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

}
