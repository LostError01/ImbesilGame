using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public static EnemyManager ObjInstance;
    public HashSet<string> enemigosDestruidos = new HashSet<string>();
    public HashSet<string> objetosDestruidos = new HashSet<string>();

    void Awake()
    {
        //Enemigos
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        //Objetos
        if (ObjInstance == null)
        {
            ObjInstance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
