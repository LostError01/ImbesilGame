using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentLevel = "Ciudad Level"; // Nombre de la escena inicial

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hacer persistente entre escenas
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
    }

    public void ResetGame()
    {
        // Acceder al valor real de currentLevel
        string currentLevel = GameManager.Instance.currentLevel;

        if (!string.IsNullOrEmpty(currentLevel))
        {
            Debug.Log($"Reiniciando escena: {currentLevel}");
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            Debug.LogError("El nombre de la escena principal no está asignado en GameManager.");
        }
    }
}