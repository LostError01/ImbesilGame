using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool jugadorEmpieza;
    private bool isPlayerTurn;

    //Escenas
    public string escenaVictoria; // Nombre de la escena al derrotar al enemigo
    public string escenaGameOver; // Nombre de la escena al morir el jugador
    public string currentScene; // Referencia a la escena actual
    public SceneTransition sceneTransition; // Referencia al script de transición de escena

    void Start()
    {
        // Inicialización de vida
        vidaMax = VidasJugador.vidaMaxima;
        vidaPlayer.maxValue = vidaMax;
        vidaPlayer.value = VidasJugador.vida;

        sliderEnemigo.maxValue = vidaMaxEnemigo;
        sliderEnemigo.value = vidaEnemigo;

        // Configuración inicial de turnos
        if (currentScene == "BattleScene01")
        {
            jugadorEmpieza = true;
            isPlayerTurn = jugadorEmpieza;
        }
        if(currentScene == "BattleScene")
        {
            jugadorEmpieza = false;
            isPlayerTurn = jugadorEmpieza;
        }

        if (!isPlayerTurn)
            StartCoroutine(EnemigoTurno());
    }

    // Método para ataques del jugador
    public void Atacar()
    {
        if (!isPlayerTurn) return; // Si no es el turno del jugador, no hacer nada

        StartCoroutine(RealizarAtaqueJugador());
    }

    // Método para defensa del jugador
    public void Defensa()
    {
        if (!isPlayerTurn) return; // Si no es el turno del jugador, no hacer nada

        StartCoroutine(RealizarDefensaJugador());
    }

    // Flujo de ataque del jugador
    IEnumerator RealizarAtaqueJugador()
    {
        isPlayerTurn = false;
        animPlayer.SetTrigger("Atacar");

        // Simular animación de ataque
        yield return new WaitForSeconds(0.5f);

        float damage = Random.Range(10f,20f);

        AplicarDaño(ref vidaEnemigo, damage, ref isDefendingEnemigo, sliderEnemigo);

        // Verificar muerte del enemigo
        if (vidaEnemigo <= 0)
        {
            sceneTransition.LoadSceneWithFade(escenaVictoria);
            yield break; // Detiene la corrutina inmediatamente
        }

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

        isDefendingEnemigo = false; // El enemigo no puede defenderse en este turno

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
            yield return new WaitForSeconds(1f);

            float damage = Random.Range(5f, 15f);
            AplicarDaño(ref VidasJugador.vida, damage, ref isDefendingPlayer, vidaPlayer);

            //Verificar muerte de jugador
            if (VidasJugador.vida <= 0)
            {
                sceneTransition.LoadSceneWithFade(escenaGameOver);
                Movimiento.Perder = true;
                yield break;
            }
        }
        else
        {
            // Defensa del enemigo
            isDefendingEnemigo = true;
            isDefendingPlayer = false; // El jugador no puede defenderse en este turno
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
            dañoFinal = dañoFinal/2f;
            defensaActiva = false; // La defensa solo dura un turno

            Debug.Log("Damage Defending = " + dañoFinal);
        }

        vida = Mathf.Max(vida - dañoFinal, 0);
        slider.value = vida;
    }
}
