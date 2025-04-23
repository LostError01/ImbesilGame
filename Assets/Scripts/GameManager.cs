using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentLevel = "Ciudad Level"; // Asegúrate de que este sea el nombre correcto

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        string currentLevel = "GameManager.Instance.currentLevel"; // O usa GameManager.Instance.currentLevel
        Debug.Log($"Reiniciando escena: {currentLevel}");
        SceneManager.LoadScene(currentLevel);
    }
}