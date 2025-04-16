using UnityEngine;

public class Escaleras : MonoBehaviour
{
    public Collider2D[] collidersMontania;
    public Collider2D[] collidersLimites;

    public int LayerPlayer;

    private void OnTriggerEnter2D(Collider2D collision) //Al entrar en trigger, desactiva el collider de la montaña para poder caminar
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D montania in collidersMontania)
            {
                montania.enabled = false;
            }

            foreach (Collider2D limite in collidersLimites)
            {
                limite.enabled = true; //Activa los limites de la montaña al entrar
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = LayerPlayer; // Cambia la capa a 15 (encima de la montaña)
        }
    }
}
