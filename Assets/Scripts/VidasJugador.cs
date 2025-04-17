using UnityEngine;
using UnityEngine.UI;

public class VidasJugador : MonoBehaviour
{
    public static float vida = 100f;
    public static float vidaMaxima = 100f;
    public Slider barraVida;

    public void Start()
    {
        //Inicializa la barra de vida
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vida;
        }
    }

    public void Update()
    {
        //Ejemplo de daño
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        vida -= damage;
        if(vida <= 0)
        {
            vida = 0;
            Muerte();
        }
        ActualizarVida();
    }

    public void ActualizarVida()
    {
        if (barraVida != null)
        {
            barraVida.value = vida;
        }
    }

    public void Muerte()
    {
        Debug.Log("El jugador ha muerto");
    }
}
