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
        // Inicializaci�n de vida
        vidaMax = VidasJugador.vidaMaxima;
        vidaPlayer.maxValue = vidaMax;
        vidaPlayer.value = VidasJugador.vida;

        sliderEnemigo.maxValue = vidaMaxEnemigo;
        sliderEnemigo.value = vidaEnemigo;

        // Configuraci�n inicial de turnos
        isPlayerTurn = jugadorEmpieza;
        if (!isPlayerTurn)
            StartCoroutine(EnemigoTurno());
    }

    // M�todo para ataques del jugador
    public void Atacar()
    {
        if (!isPlayerTurn) return;

        StartCoroutine(RealizarAtaqueJugador());
    }

    // M�todo para defensa del jugador
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

        // Simular animaci�n de ataque
        yield return new WaitForSeconds(0.5f);

        float damage = Random.Range(10f, 20f);
        AplicarDa�o(ref vidaEnemigo, damage, ref isDefendingEnemigo, sliderEnemigo);

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
        // L�gica de IA del enemigo (50% chance de atacar/defender)
        if (Random.value > 0.5f)
        {
            // Ataque del enemigo
            animEnemigo.SetTrigger("Atacar");
            yield return new WaitForSeconds(0.5f);

            float damage = Random.Range(5f, 15f);
            AplicarDa�o(ref VidasJugador.vida, damage, ref isDefendingPlayer, vidaPlayer);
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

    // M�todo universal para aplicar da�o
    void AplicarDa�o(ref float vida, float da�oBase,
                    ref bool defensaActiva, Slider slider)
    {
        float da�oFinal = da�oBase;

        // Aplicar reducci�n por defensa
        if (defensaActiva)
        {
            da�oFinal *= 0.5f;
            defensaActiva = false; // La defensa solo dura un turno
        }

        vida = Mathf.Max(vida - da�oFinal, 0);
        slider.value = vida;
    }
}
