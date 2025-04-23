using UnityEngine;

public class BossSpawnPointManager : MonoBehaviour
{
    private void Awake()
    {
        // Asegurarse de que solo exista una instancia del BossSpawnPoint
        if (FindObjectsOfType<BossSpawnPointManager>().Length > 1)
        {
            Debug.LogWarning("Se detectó una segunda instancia de BossSpawnPoint. Destruyendo...");
            Destroy(gameObject);
            return;
        }

        // Persistir el BossSpawnPoint entre escenas
        DontDestroyOnLoad(gameObject);

        Debug.Log("BossSpawnPoint inicializado y configurado para persistir entre escenas.");
    }

    private void OnEnable()
    {
        // Asignar este BossSpawnPoint al EnemyManager si existe
        AssignToEnemyManager();
    }

    private void AssignToEnemyManager()
    {
        // Buscar el EnemyManager en la escena actual
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.AssignBossSpawnPoint(transform);
            Debug.Log("BossSpawnPoint asignado correctamente al EnemyManager.");
        }
        else
        {
            Debug.LogError("No se encontró ningún EnemyManager en la escena.");
        }
    }
}