using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMining : MonoBehaviour {

    // general timer to know the time between two update
    float timer;

    bool playerInRange;
    GameObject player;
    PlayerMining playerMining;

    public GameObject flyingCrystal;

    // the crystal sphere collider
    SphereCollider sc;

    // the amont of crystal still to mine
    public float remainingCrystal = 10f;

    // timer to mine
    float miningSpeed;
    // The amont of cristal per timeBetweenCristal the player is mining
    float miningAmont;


    // Use this for initialization
    void Start () {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMining = player.GetComponent<PlayerMining>();

        miningAmont = playerMining.miningAmont;
        miningSpeed = playerMining.miningSpeed;

        // Create the sphere collider, radius = 2, isTrigger = true (we can go through it)
        sc = gameObject.GetComponent<SphereCollider>() as SphereCollider;
    }
	
	// Update is called once per frame
	void Update () {

        // we update the timer
        timer += Time.deltaTime;

        if (playerInRange && remainingCrystal > 0)
        {
            playerMining.isMining = true;
            if (timer > miningSpeed)
            {
                Mine();
            }
        }
        else
        {
            playerMining.isMining = false;
        }
	}

    void OnTriggerEnter(Collider sc)
    {
        if (sc.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider sc)
    {
        if (sc.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Mine ()
    {

        if (timer > miningSpeed)
        {
            remainingCrystal -= miningAmont;
            timer = 0f;
        }

        // we launch the floating crystal
        GameObject floatingCrystal = (GameObject)Instantiate(flyingCrystal, transform.position, new Quaternion(0,0,0,0));
        floatingCrystal.gameObject.name = "FloatingCrystal";

    }
}
