using System.Collections.Generic;
using UnityEngine;

public class EnemyManager3 : MonoBehaviour
{
    public static EnemyManager3 Instance;

    // Listas para gestionar enemigos y objetos
    public HashSet<string> enemigosDestruidos = new HashSet<string>();
    public HashSet<string> objetosDestruidos = new HashSet<string>();

    public static bool bossSpawned = false; // Indica si el jefe ya ha sido spawneado

    // Enemigos destruidos
    public int EnemigosADestruir = 3;
    public static int EnemyCount = 0; // Contador de enemigos

    private void FixedUpdate()
    {
        //Verificar si se ha destruido el número necesario de enemigos
        if (EnemyCount >= EnemigosADestruir && !bossSpawned)
        {
            bossSpawned = true; // Evitar que el jefe se spawnee nuevamente
            Debug.Log("bossSpawned = " + bossSpawned);
        }
    }
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
            Debug.LogWarning("Se detectó una segunda instancia de EnemyManager. Destruyendo..." + EnemyCount);
            Destroy(gameObject);
            EnemyCount++;
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
