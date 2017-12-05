using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

    public GameObject barrel;
    public GameObject effect;
    public int explosionDamage = 100;
    public float timeToDestroyBarril = 1f;
    public float additionalEffectTime = 3f;

    // to avoid trying to make a barrel explode multiple times
    // (causes infinite loops)
    bool hasExploded;
    // is the barrel ready to be destroyed
    bool discardBarrel;
    // list of all the object in range
    IList<GameObject> objectInRange = new List<GameObject>();

    // Use this for initialization
    void Start () {
        effect.SetActive(false);
        discardBarrel = false;
        hasExploded = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (discardBarrel)
        {
            Destroy(this.gameObject, timeToDestroyBarril + additionalEffectTime);
        }
	}

    public void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            effect.SetActive(true);
            barrel.SetActive(false);
            Destroy(effect, timeToDestroyBarril + additionalEffectTime);

            DamageAllInRange();
        }
    }

    void DamageAllInRange()
    {
        // object in range take damage
        foreach (var other in objectInRange)
        {
            if (other != null)
            {
                if (other.gameObject.tag == "Player")
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(explosionDamage);
                }
                else if (other.gameObject.tag == "Enemy")
                {
                    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(explosionDamage);
                    }
                }
                else if (other.gameObject.tag == "Explosive")
                {
                    ExplosiveBarrel explosiveBarrel = other.GetComponent<ExplosiveBarrel>();
                    explosiveBarrel.Explode();
                }
            }
        }

        discardBarrel = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // we add the moving object to the list of objects in range
        if (other.gameObject != this.gameObject && !objectInRange.Contains(other.gameObject))
        {
            objectInRange.Add(other.gameObject);
            //print(other.gameObject.tag + " in barrel range");
        }  
    }

    void OnTriggerExit(Collider other)
    {
        // we remove the moving object from the list of objects in range
        objectInRange.Remove(other.gameObject);
        //print(other.gameObject.tag + " out barrel range");
    }
}
