using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public string destroyState;//nombre
    public float timeForDisable;//tiempo desactiva collider
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack")// circle collider 2D
        {
            anim.Play(destroyState);
            yield return new WaitForSeconds
            (timeForDisable);
            foreach
            (Collider2D collider in
            GetComponents<Collider2D>())
            {
                collider.enabled = false; //quitar collider
            }
        }
    }
    void Update()
    {
        AnimatorStateInfo stateInfo =
        anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(destroyState)
        && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}    