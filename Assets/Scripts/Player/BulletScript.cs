using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // damage per bullet
    public int damagePerShot = 5;

    // energyCost (to change depending the bullet in the unity editor)
    public int energyCost = 1;

    // burning config
    public int burningDamage = 2;
    public float burningTotalTime = 2.0f;
    public float burningInterval = 0.9f;

    // frozen config
    public int frozenSlow = 3;
    public float frozenTotalTime = 1.5f;

    // earth config
    public int lifeSteal = 10;

    // animation on the bullet impact
    public GameObject bulletImpact;

    // different kind of bullet
    public enum BulletType
    {
        Normal,
        Fire,
        Ice,
        Earth
    }

    // kind of this bullet
    public BulletType typeOfBullet;


    void Start () {
        // destory the bullet 4 sec after the shoot 
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
                enemyHealth.TakeDamage(this);
            }
        }
        else if (other.tag == "Explosive" && !other.isTrigger)
        {
            ExplosiveBarrel barrel = other.GetComponent<ExplosiveBarrel>();
            barrel.Explode();
        }
        else if (other.tag == "Barrelwall")
        {
            PassWallTrigger passWall = other.GetComponentInParent<PassWallTrigger>();
            passWall.PassOnEffect();
        }
        else if (other.tag == "Firewall" && typeOfBullet==BulletType.Ice)
        {
            ZoneWall zoneWall = other.GetComponent<ZoneWall>();
            zoneWall.DesactivateWall();
        }
        else if (other.tag == "Plantwall" && typeOfBullet == BulletType.Fire)
        {
            ZoneWall zoneWall = other.GetComponent<ZoneWall>();
            zoneWall.DesactivateWall();
        }
        else
        {
            // for other objects
        }

        //print(other.tag);
        if (other.tag == "Player" || other.tag == "FloatingCrystal")
        {
            // don't destroy it when it's the player shooting (or the bullet will never go out of the player collider...)
        }
        else if (other.tag == "Explosive" && other.isTrigger)
        {
            // don't destroy if it impacts the trigger collider of an explosive barrel
        }
        else if (other.tag == "Enemy" && other.isTrigger)
        {
            // don't destroy if it impacts the trigger collider of an enemy (EnemyFOV)
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
