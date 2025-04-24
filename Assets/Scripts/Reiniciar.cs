using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Nombre de la escena del primer nivel (cámbialo al nombre de tu primer nivel)
    public string firstLevelName = "Ciudad Level";

   
    public void RestartFromFirstLevel()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    
    public void RestartCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}