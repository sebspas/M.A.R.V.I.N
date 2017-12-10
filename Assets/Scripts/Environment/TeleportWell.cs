using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWell : MonoBehaviour {

    GameObject player;
    public GameObject teleporter;
    Vector3 teleportPos;
    bool playerInRange;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportPos = teleporter.transform.position;
        playerInRange = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerInRange && Input.GetKey(KeyCode.Space))
        {
            player.transform.position = teleportPos;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
