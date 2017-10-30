using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 4);
    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        if (other.tag == "Player")
        {
            // don't destroy it when it's the player shooting (or the bullet will never go out of the player collider...)
        } else
        {
            // in all other case destroy it
            Destroy(this.gameObject);
        }
       
    }
}
