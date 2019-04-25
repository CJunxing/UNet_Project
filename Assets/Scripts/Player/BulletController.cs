using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag == "Player")
        {
            hit.GetComponent<Health>().TakeDamage(10);
        }
        else if (hit.tag == "Enemy")
        {
            hit.GetComponent<EnemyHealth>().TakeDamage(10);
        }
       

        Destroy(gameObject);
    }
}
