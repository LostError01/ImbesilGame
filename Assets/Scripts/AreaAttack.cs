using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AreaAttack : MonoBehaviour
{
    public SceneTransition transition;
    void OnTriggerEnter2D(Collider2D col)
    {
        // Verificar trigger con el BoxCollider2D 'enemigo'
        if (col.CompareTag("Enemy"))
        {
            //Destruir el objeto del enemigo
            Destroy(col.gameObject);
        }
    }
}
