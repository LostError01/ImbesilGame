using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;

    private void Start()
    {
        // Inicializar la imagen de fade con opacidad 0
        fadeImage.canvasRenderer.SetAlpha(0f);
    }

    // Método público para cargar una escena con fade
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        fadeImage.enabled = true;

        Debug.Log($"Intentando cargar la escena: {sceneName}");

        // Verificar si la escena existe en el Build Settings
        if (!IsSceneInBuildSettings(sceneName))
        {
            Debug.LogError($"La escena '{sceneName}' no está en el Build Settings. No se puede cargar.");
            yield break; // Detener la ejecución
        }

        // Fade a negro
        fadeImage.CrossFadeAlpha(1f, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);

        // Cargar la escena
        Debug.Log($"Cargando escena: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    // Método auxiliar para verificar si una escena está en el Build Settings
    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuild = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameInBuild == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}