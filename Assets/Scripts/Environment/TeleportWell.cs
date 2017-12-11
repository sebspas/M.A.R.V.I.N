using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWell : MonoBehaviour {

    GameObject player;
    public GameObject textUI;
    public GameObject teleporter;
    Vector3 teleportPos;
    bool playerCanInteract;


	// Use this for initialization
	void Start () {
        //textUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        teleportPos = teleporter.transform.position;
        playerCanInteract = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerCanInteract && Input.GetKey(KeyCode.Space))
        {
            player.transform.position = teleportPos;
        }

    }

    // the player must have finish the forest zone boss
    // to be able to interact with the well
    private bool isInteractible()
    {
        return player.GetComponent<PlayerShooting>().playerGotAllWeapon();
    }

    // player is within range and can interact
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isInteractible())
        {
            playerCanInteract = true;
            textUI.GetComponent<Animator>().SetBool("appearWell", true);
        }
    }

    // player is out of range therefore can't interact
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanInteract = false;
            textUI.GetComponent<Animator>().SetBool("appearWell", false);
        }
    }
}
