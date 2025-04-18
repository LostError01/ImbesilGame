using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AreaAttack : MonoBehaviour
{
    public SceneTransition transition;
    public BoxCollider2D enemigo;
    public Rigidbody2D rbEnemy;
    void OnTriggerEnter2D(Collider2D col)
    {
        // Verificar trigger con el BoxCollider2D 'enemigo'
        if (col.Equals(enemigo))
        {
            // Desactivar el Rigidbody2D del enemigo
            rbEnemy.linearVelocity = Vector2.zero;

            //Destruir el objeto del enemigo
            Destroy(col.gameObject);

            transition.LoadSceneWithFade("BattleScene01");
        }
    }
}
