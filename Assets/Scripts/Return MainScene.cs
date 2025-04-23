using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScene : MonoBehaviour
{
    public SceneTransition transition;

    public void ReturnToMain()
    {
        string currentLevel = GameManager.Instance.currentLevel;

        if (!string.IsNullOrEmpty(currentLevel))
        {
            Debug.Log($"Regresando a la escena principal: {currentLevel}");
            transition.LoadSceneWithFade(currentLevel);
        }
    }
}