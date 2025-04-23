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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckForBossSpawn()
    {
        if (enemigosDestruidos.Count >= 3 && !bossSpawned && bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            bossSpawned = true;
        }
    }

    public void ResetGame()
    {
        enemigosDestruidos.Clear();
        objetosDestruidos.Clear();
        bossSpawned = false;
    }
}