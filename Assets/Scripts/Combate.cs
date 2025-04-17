using UnityEngine;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    private float vidaMax;
    public Slider vidaPlayer;
    public Animator animPlayer;

    void Start()
    {
        // Obtén la vida máxima del jugador desde el script global
        vidaMax = VidasJugador.vidaMaxima;

        if (vidaPlayer != null)
        {
            vidaPlayer.maxValue = vidaMax; //Vida Maxima
            vidaPlayer.value = VidasJugador.vida; //Vida Actual Global
        }
    }

    void Update()
    {
        // Actualiza el slider cada frame para reflejar cambios en tiempo real
        if (vidaPlayer != null)
        {
            vidaPlayer.value = VidasJugador.vida;
        }
    }

    public void Atacar()
    {
        animPlayer.SetTrigger("Atacar");
    }

    public void Defensa()
    {
        animPlayer.SetTrigger("Defender");
    }
}
