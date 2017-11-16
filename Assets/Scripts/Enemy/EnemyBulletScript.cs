using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    // damage per bullet
    public int damagePerShot = 10;

    // animation on the bullet impact
    public GameObject bulletImpact;

    public float timeBfDestroy = 4.0f;


    void Start()
    {
        // destory the bullet 4 sec after the shoot 
        Destroy(this.gameObject, timeBfDestroy);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Try and find an PlayerHealth script on the gameobject hit.
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If the PlayerHealth component exist...
            if (playerHealth != null)
            {
                // ... the player should take damage.
                playerHealth.TakeDamage(damagePerShot);
            }
        }
        else if (other.tag == "Explosive" && !other.isTrigger)
        {
            ExplosiveBarrel barrel = other.GetComponent<ExplosiveBarrel>();
            barrel.Explode();
        }
        else
        {
            // for other objects
        }

        //print(other.tag);
        if (other.tag == "Enemy" || other.tag == "FloatingCrystal" || other.tag == "TriggerEffect" || other.tag == "Sight")
        {
            // don't destroy it when it's the player shooting (or the bullet will never go out of the player collider...)
        }
        else if (other.tag == "Explosive" && other.isTrigger)
        {
            // don't destroy if it impacts the trigger collider of an explosive barrel
        }
        else
        {
            // in all other case destroy it
            GameObject impactAnim = (GameObject)Instantiate(bulletImpact, this.transform.position, Quaternion.identity);
            // destroy the anim after 1 sec
            Destroy(impactAnim, 1.2f);

            Destroy(this.gameObject);
        }
    }

}
