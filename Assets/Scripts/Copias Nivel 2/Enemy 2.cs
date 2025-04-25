using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public Animator animEnemy;
    public Transform playerTarget;
    public float vel;

    private bool isChasing;

    public SceneTransition transition;

    // Instancias
    public string idEnemigo;

    // Escena de combate específica para este enemigo
    public string battleSceneName; // Nombre de la escena de combate

    // Strings para el Animator
    public string Horizontal = "E_Horizontal";
    public string Vertical = "E_Vertical";
    public string Walking = "E_Walking";

    private void Start()
    {
        // Buscar al jugador automáticamente si no está asignado
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTarget = player.transform;
            }
            else
            {
                Debug.LogError("No se encontró un jugador con el tag 'Player'.");
            }
        }

        // Si este enemigo ya fue destruido, eliminarse inmediatamente
        if (EnemyManager2.Instance.enemigosDestruidos.Contains(idEnemigo) && Movimiento.Perder == false)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        // Si el objeto se destruye manualmente (no por cambio de escena), registrar su ID
        if (gameObject.scene.isLoaded && Movimiento.Perder == false)
        {
            EnemyManager2.Instance.enemigosDestruidos.Add(idEnemigo);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("El jugador no está asignado. Verifica el campo Player Target.");
            return;
        }

        if (isChasing)
        {
            Vector2 direccion = (playerTarget.position - transform.position).normalized;

            // Variables para la animación
            animEnemy.SetFloat(Horizontal, direccion.x);
            animEnemy.SetFloat(Vertical, direccion.y);
            animEnemy.SetBool(Walking, true);

            // Mover al enemigo
            rbEnemy.linearVelocity = new Vector2(direccion.x, direccion.y) * vel;
        }
        else
        {
            animEnemy.SetBool(Walking, false);
            rbEnemy.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activar la persecución cuando el jugador entra en el rango
            isChasing = true;

            Debug.Log("El jugador ha entrado en el rango del enemigo. Activando persecución.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactivar la persecución cuando el jugador sale del rango
            isChasing = false;
            rbEnemy.linearVelocity = Vector2.zero;

            Debug.Log("El jugador ha salido del rango del enemigo. Deteniendo persecución.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detener al enemigo cuando el jugador entra en contacto
            if (rbEnemy != null)
            {
                rbEnemy.linearVelocity = Vector2.zero;
            }

            isChasing = false;

            // Guardar el nombre del nivel actual
            if (GameManager.Instance != null)
            {
                string currentLevel = SceneManager.GetActiveScene().name;
                GameManager.Instance.currentLevel = currentLevel;
            }
            else
            {
                Debug.LogError("GameManager.Instance es nulo. Asegúrate de tener un objeto con el script GameManager en la escena.");
            }

            // Registrar el enemigo como derrotado
            if (gameObject.scene.isLoaded && Movimiento.Perder == false)
            {
                EnemyManager2.Instance.enemigosDestruidos.Add(idEnemigo);
            }

            // Destruir el objeto del enemigo
            Destroy(gameObject);

            // Iniciar la transición a la escena de combate asignada
            if (!string.IsNullOrEmpty(battleSceneName) && transition != null)
            {
                Debug.Log($"Cargando escena de combate: {battleSceneName}");
                transition.LoadSceneWithFade(battleSceneName);
            }
            else
            {
                Debug.LogError("La escena de combate no está asignada o el componente SceneTransition es nulo.");
            }
        }
    }
}
