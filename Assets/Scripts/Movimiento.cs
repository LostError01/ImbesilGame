using System.Collections;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public Rigidbody2D rb; // Referencia al Rigidbody2D del objeto
    public Animator animPlayer; // Referencia al Animator del objeto
    private bool isAttacking = false; // Bandera para controlar el ataque

    // Variables para el ataque
    public float attackCooldown = 0.5f; // Tiempo entre ataques
    private float lastAttackTime = 0f;
    public CircleCollider2D areaAttack; // Referencia al área de ataque

    private Vector2 movimiento;

    private void Update()
    {
        // Detectar click izquierdo para atacar
        if (Input.GetMouseButtonDown(0) && !isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
        
        //Area de ataque
        if(isAttacking == false)
        {
            areaAttack.enabled = false; // Desactivar el área de ataque
        }
        else
        {
            areaAttack.enabled = true; // Activar el área de ataque
        }
    }
    private void FixedUpdate()
    {
        if (!isAttacking)
        {

            movimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Obtener la magnitud del movimiento

            if (movimiento != Vector2.zero)
            {
                animPlayer.SetFloat("Horizontal", movimiento.x); // Actualizar parámetro "Horizontal" en el Animator
                animPlayer.SetFloat("Vertical", movimiento.y); // Actualizar parámetro "Vertical" en el Animator
                animPlayer.SetBool("Walking", true);
            }
            else
            {
                animPlayer.SetBool("Walking", false); // Si no hay movimiento, establecer "Walking" en falso
            }

            rb.linearVelocity = new Vector2(movimiento.x, movimiento.y) * speed; // Aplicar velocidad al Rigidbody2D

        }
    }

    void Attack()
    {
        isAttacking = true; // Establecer la bandera de ataque en verdadero
        lastAttackTime = Time.time; // Registrar el tiempo del último ataque

        //Activar animacion de Ataque
        animPlayer.SetTrigger("Attack");

        StartCoroutine(ResetAttack()); // Iniciar la coroutine para restablecer el ataque
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Esperar el tiempo de recarga
        isAttacking = false; // Restablecer la bandera de ataque
    }

    public void ResetAttackTrigger()
    {
        animPlayer.ResetTrigger("Attack");
    }
}
