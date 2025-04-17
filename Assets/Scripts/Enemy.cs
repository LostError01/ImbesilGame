using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public Animator animEnemy;
    private Transform playerTarget;
    public float vel;

    private bool isChasing;

    public SceneTransition transition;

    // Update is called once per frame
    void Update()
    {
        Vector2 direccion = (playerTarget.position - transform.position).normalized;

        //Variables para la animacion
        if(direccion != Vector2.zero && isChasing == true)
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

        if(collision.gameObject.CompareTag("AreaAttack"))
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

            transition.LoadSceneWithFade("BattleScene");
        }
    }
}
