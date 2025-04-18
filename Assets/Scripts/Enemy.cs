using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public Animator animEnemy;
    public Transform playerTarget;
    public float vel;

    private bool isChasing;

    public SceneTransition transition;

    //Instancias
    public string idEnemigo;

    private void Start()
    {
        // Si este enemigo ya fue destruido, eliminarse inmediatamente
        if (EnemyManager.Instance.enemigosDestruidos.Contains(idEnemigo) && Movimiento.Perder == false)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        // Si el objeto se destruye manualmente (no por cambio de escena), registrar su ID
        if (gameObject.scene.isLoaded && Movimiento.Perder == false)
        {
            EnemyManager.Instance.enemigosDestruidos.Add(idEnemigo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direccion = (playerTarget.position - transform.position).normalized;

        //Variables para la animacion
        if (direccion != Vector2.zero && isChasing == true)
        {
            animEnemy.SetFloat("E_Horizontal", direccion.x);
            animEnemy.SetFloat("E_Vertical", direccion.y);
            animEnemy.SetBool("E_Walking", true);
        }
        else
        {
            animEnemy.SetBool("E_Walking", false);
        }

        if (isChasing == true)
        {
            rbEnemy.linearVelocity = new Vector2(direccion.x, direccion.y) * vel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerTarget == null)
            {
                playerTarget = collision.transform;
            }
            isChasing = true;
        }

        if (collision.gameObject.CompareTag("AreaAttack"))
        {
            rbEnemy.linearVelocity = Vector2.zero;
            isChasing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rbEnemy.linearVelocity = Vector2.zero;
            isChasing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rbEnemy.linearVelocity = Vector2.zero;
            isChasing = false;
            //Destruir el objeto del enemigo
            Destroy(gameObject);

            transition.LoadSceneWithFade("BattleScene");
        }
    }
}
