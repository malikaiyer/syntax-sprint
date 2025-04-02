using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMapScene()
    {
        SceneManager.LoadScene("map"); 
    }
}
