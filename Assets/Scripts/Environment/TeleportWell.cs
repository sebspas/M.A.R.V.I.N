using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWell : MonoBehaviour {

    public GameObject textUI;
    public GameObject teleporter;
    public GameObject teleportFX;

    GameObject player;
    private Vector3 teleportPos;
    private bool playerCanInteract;
    private AudioSource teleportSound;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportPos = teleporter.transform.position;
        teleportSound = GetComponent<AudioSource>();
        playerCanInteract = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerCanInteract && Input.GetKey(KeyCode.Space))
        {
            Teleport();
        }

    }

    private void Teleport()
    {
        teleportSound.Play();
        player.transform.position = teleportPos;
        // in all other case destroy it
        GameObject effect = (GameObject)Instantiate(teleportFX, teleportPos, Quaternion.identity);
        // destroy the anim after 1 sec
        Destroy(effect, 3.5f);
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
