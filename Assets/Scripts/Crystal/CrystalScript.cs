using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour {

    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    Collider playerCollider;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        // destroy the floating crystal after 5s if it doesn't find the player
        Destroy(this.gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update () {

        nav.SetDestination(player.position);
        this.transform.LookAt(player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            Destroy(this.gameObject);
        }
    }
}
