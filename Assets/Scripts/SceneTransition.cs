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

        Debug.Log($"Iniciando fade para cargar la escena: {sceneName}");

        // Fade a negro
        fadeImage.CrossFadeAlpha(1f, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);

        // Cargar la escena
        Debug.Log($"Cargando escena: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}