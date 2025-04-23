using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    // Listas para gestionar enemigos y objetos
    public HashSet<string> enemigosDestruidos = new HashSet<string>();
    public HashSet<string> objetosDestruidos = new HashSet<string>();

    // Variables para el jefe final
    [SerializeField] private GameObject bossPrefab; // Prefab del jefe final
    [SerializeField] private Transform bossSpawnPoint; // Punto donde aparecerá el jefe

    private bool bossSpawned = false; // Indica si el jefe ya ha sido spawneado

    void Awake()
    {
        // Singleton pattern: Asegurarse de que solo exista una instancia
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
            Debug.Log("EnemyManager inicializado correctamente.");
        }
        else
        {
            Debug.LogWarning("Se detectó una segunda instancia de EnemyManager. Destruyendo...");
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Buscar dinámicamente el BossSpawnPoint en la escena actual
        GameObject bossSpawnObject = GameObject.Find("BossSpawnPoint");
        if (bossSpawnObject != null)
        {
            bossSpawnPoint = bossSpawnObject.transform;
            Debug.Log("BossSpawnPoint encontrado y asignado correctamente.");
        }
        else
        {
            Debug.LogError("No se encontró ningún GameObject llamado 'BossSpawnPoint' en la escena.");
        }
    }

    public void CheckForBossSpawn()
    {
        // Validar que el prefab y el punto de spawn estén asignados
        if (bossPrefab == null)
        {
            Debug.LogError("El prefab del jefe final no está asignado en el EnemyManager.");
            return;
        }

        if (bossSpawnPoint == null)
        {
            Debug.LogError("El punto de spawn del jefe final no está asignado en el EnemyManager.");
            return;
        }

        // Verificar si se han derrotado los 3 enemigos y si el jefe aún no ha sido spawneado
        if (enemigosDestruidos.Count >= 3 && !bossSpawned)
        {
            Debug.Log("Todos los enemigos han sido derrotados. Spawneando al jefe final.");

            // Instanciar al jefe final en la posición especificada
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

            // Marcar que el jefe ha sido spawneado
            bossSpawned = true;
        }
        else
        {
            Debug.LogWarning($"Aún faltan enemigos por derrotar. Derrotados: {enemigosDestruidos.Count}, Total necesario: 3");
        }
    }
    public void AssignBossSpawnPoint(Transform spawnPoint)
    {
        if (spawnPoint != null)
        {
            bossSpawnPoint = spawnPoint;
            Debug.Log("BossSpawnPoint asignado dinámicamente desde el BossSpawnPointManager.");
        }
        else
        {
            Debug.LogError("El BossSpawnPoint proporcionado es nulo.");
        }
    }
    public void ResetGame()
    {
        // Limpiar las listas y reiniciar el estado del juego
        enemigosDestruidos.Clear();
        objetosDestruidos.Clear();
        bossSpawned = false;

        Debug.Log("Estado del juego reiniciado. Listas limpiadas y jefe final desactivado.");
    }
}