    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public int damagePerShot = 5;

    public GameObject bulletImpact;

    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, 4);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();         

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
        else
        {
            // for other objects
        }

        //print(other.tag);
        if (other.tag == "Player")
        {
            // don't destroy it when it's the player shooting (or the bullet will never go out of the player collider...)
        } else
        {
            // in all other case destroy it
            GameObject impactAnim = (GameObject)Instantiate(bulletImpact, this.transform.position, Quaternion.identity);
            // destroy the anim after 1 sec
            Destroy(impactAnim, 1.2f);

            Destroy(this.gameObject);
        }
    }

}
