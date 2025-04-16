using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public Rigidbody2D rb; // Referencia al Rigidbody2D del objeto
    public Animator animPlayer; // Referencia al Animator del objeto
    private void FixedUpdate()
    {
        Vector2 movimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Obtener la magnitud del movimiento

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
