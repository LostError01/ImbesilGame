using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    // Jugador
    private float vidaMax;
    public Slider vidaPlayer;
    public Animator animPlayer;
    private bool isDefendingPlayer = false;

    // Enemigo
    private float vidaMaxEnemigo = 100f;
    private float vidaEnemigo = 100f;
    public Slider sliderEnemigo;
    public Animator animEnemigo;
    private bool isDefendingEnemigo = false;

    // Control de turnos
    public bool jugadorEmpieza = true;
    private bool isPlayerTurn;

    void Start()
    {
        // Inicialización de vida
        vidaMax = VidasJugador.vidaMaxima;
        vidaPlayer.maxValue = vidaMax;
        vidaPlayer.value = VidasJugador.vida;

        sliderEnemigo.maxValue = vidaMaxEnemigo;
        sliderEnemigo.value = vidaEnemigo;

        // Configuración inicial de turnos
        isPlayerTurn = jugadorEmpieza;
        if (!isPlayerTurn)
            StartCoroutine(EnemigoTurno());
    }

    // Método para ataques del jugador
    public void Atacar()
    {
        if (!isPlayerTurn) return;

        StartCoroutine(RealizarAtaqueJugador());
    }

    // Método para defensa del jugador
    public void Defensa()
    {
        if (!isPlayerTurn) return;

        StartCoroutine(RealizarDefensaJugador());
    }

    // Flujo de ataque del jugador
    IEnumerator RealizarAtaqueJugador()
    {
        isPlayerTurn = false;
        animPlayer.SetTrigger("Atacar");

        // Simular animación de ataque
        yield return new WaitForSeconds(0.5f);

        float damage = Random.Range(10f, 20f);
        AplicarDaño(ref vidaEnemigo, damage, ref isDefendingEnemigo, sliderEnemigo);

        // Esperar antes de turno enemigo
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemigoTurno());
    }

    // Flujo de defensa del jugador
    IEnumerator RealizarDefensaJugador()
    {
        isPlayerTurn = false;
        isDefendingPlayer = true;
        animPlayer.SetTrigger("Defender");

        // Esperar antes de turno enemigo
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemigoTurno());
    }

    // Turno del enemigo
    IEnumerator EnemigoTurno()
    {
        // Lógica de IA del enemigo (50% chance de atacar/defender)
        if (Random.value > 0.5f)
        {
            // Ataque del enemigo
            animEnemigo.SetTrigger("Atacar");
            yield return new WaitForSeconds(0.5f);

            float damage = Random.Range(5f, 15f);
            AplicarDaño(ref VidasJugador.vida, damage, ref isDefendingPlayer, vidaPlayer);
        }
        else
        {
            // Defensa del enemigo
            isDefendingEnemigo = true;
            animEnemigo.SetTrigger("Defender");
            yield return new WaitForSeconds(1f);
        }

        // Volver al turno del jugador
        isPlayerTurn = true;
    }

    // Método universal para aplicar daño
    void AplicarDaño(ref float vida, float dañoBase,
                    ref bool defensaActiva, Slider slider)
    {
        float dañoFinal = dañoBase;

        // Aplicar reducción por defensa
        if (defensaActiva)
        {
            dañoFinal *= 0.5f;
            defensaActiva = false; // La defensa solo dura un turno
        }

        vida = Mathf.Max(vida - dañoFinal, 0);
        slider.value = vida;
    }
}
