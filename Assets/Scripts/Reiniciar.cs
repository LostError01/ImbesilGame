using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    private string nombreEscena = "SampleScene"; // Nombre de la escena actual
    public void Reinicio()
    {
        SceneManager.LoadScene(nombreEscena); // Reiniciar la escena actual
    }
}
