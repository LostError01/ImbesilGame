using System.Collections;
using UnityEngine;

public class DestruirObj : MonoBehaviour
{
    private Animator rockAnimator;

    private void Start()
    {
        rockAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto colisionado tiene la etiqueta "Player"
        if (collision.CompareTag("AreaAttack"))
        {
            // Destruir el objeto al que este script está adjunto
            rockAnimator.SetTrigger("Destruido");
            StartCoroutine(DestruirRoca());
        }
    }

    IEnumerator DestruirRoca()
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(0.5f);
        // Destruir el objeto
        Destroy(gameObject);
    }
}
