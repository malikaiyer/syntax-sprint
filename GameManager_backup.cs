// using UnityEngine;

// public class GameManager : MonoBehaviour
// {
//     public GameObject[] bugs;
//     public RobotController robot;


//     public int score;
//     public int lives;

//     private void Start()
//     {
//         NewGame();

//     }

//     private void Update()
//     {
//         if (this.lives <= 0 && Input.anyKeyDown)
//         { NewGame(); }
//     }
//     private void NewGame()
//     {
//         SetScore(0);
//         SetLives(3);
//         NewRound();
//     }

//     private void NewRound()
//     {

//         ResetState();
//     }

//     private void ResetState()
//     {
//         for (int i = 0; i < this.bugs.Length; i++)
//         {
//             this.bugs[i].GetComponent<Bug>().ResetState();
//         }
//         this.robot.gameObject.SetActive(true);
//     }
//     private void GameOver()
//     {
//         for (int i = 0; i < this.bugs.Length; i++)
//         {
//             this.bugs[i].gameObject.SetActive(false);
//         }
//         this.robot.gameObject.SetActive(false);
//     }
//     private void SetScore(int score)
//     {
//         this.score = score;

//     }

//     private void SetLives(int lives)
//     {
//         this.lives = lives;
//     }

//     public void RobotEaten()
//     {
//         this.robot.gameObject.SetActive(false);
//         SetLives(this.lives - 1);
//         if (this.lives > 0)
//         {
//             Invoke(nameof(ResetState), 3.0f);
//         }
//         else
//         {
//             GameOver();
//         }
//     }

// }


//New Attempt with heartsscript is working the the hearts going away
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] bugs;
    public RobotController robot;
    public Heart1Script heartManager;

    public int score;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (heartManager.GetCurrentLives() <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        heartManager.ResetHearts();
        NewRound();
    }

    private void NewRound()
    {
        ResetState();
    }

    private void ResetState()
    {
        foreach (GameObject bug in bugs)
        {
            bug.GetComponent<Bug>().ResetState();
        }

        if (robot != null)
        {
            robot.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        foreach (GameObject bug in bugs)
        {
            bug.SetActive(false);
        }

        if (robot != null)
        {
            robot.gameObject.SetActive(false);
        }
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    public void RobotEaten()
    {
        robot.gameObject.SetActive(false);
        heartManager.LoseLife();

        if (heartManager.GetCurrentLives() > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }
}

//Replaced the internal lives variable
//Replaced all lives references with heartManager.GetCurrentLives()
//Integrated ResetHearts() into NewGame()
//Replaced SetLives(this.lives - 1) with heartManager.LoseLife()