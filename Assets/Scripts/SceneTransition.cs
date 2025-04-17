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
        fadeImage.canvasRenderer.SetAlpha(0f);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        fadeImage.enabled = true;

        // Fade a negro
        fadeImage.CrossFadeAlpha(1f, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);

        // Cargar la escena
        SceneManager.LoadScene(sceneName);
    }
}
