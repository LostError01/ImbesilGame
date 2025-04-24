using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab del jefe

    private bool Spawned = false; // Indica si el jefe ya ha sido spawneado

    private void Start()
    {
        bossPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyManager.bossSpawned && !Spawned)
        {
            Spawned = true; // Evitar que el jefe se spawnee nuevamente
            bossPrefab.SetActive(true); // Activar el prefab del jefe
        }
    }
}
