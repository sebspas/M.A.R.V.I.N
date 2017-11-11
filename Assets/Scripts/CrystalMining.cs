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

    // End of crystal animations:
    public GameObject crystalAura;
    //public GameObject crystalEnv;

    // list of all the enemies when the crystal is ended
    public GameObject[] allEnemies;
    public GameObject fire;

    // the crystal sphere collider
    SphereCollider sc;

    // the amont of crystal still to mine
    public float remainingCrystal = 10f;

    // timer to mine
    float miningSpeed;
    // The amont of cristal per timeBetweenCristal the player is mining
    float miningAmont;
    bool isEmpty;


    // Use this for initialization
    void Start () {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMining = player.GetComponent<PlayerMining>();

        miningAmont = playerMining.miningAmont;
        miningSpeed = playerMining.miningSpeed;

        // Create the sphere collider, radius = 2, isTrigger = true (we can go through it)
        sc = gameObject.GetComponent<SphereCollider>() as SphereCollider;
        isEmpty = false;
    }
	
	// Update is called once per frame
	void Update () {

        // we update the timer
        timer += Time.deltaTime;

        if (!isEmpty)
        {
            if (playerInRange)
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
            if (remainingCrystal <= 0 && !isEmpty)
            {
                playerMining.isMining = false;
                endOfCrystal();
            }
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

    void endOfCrystal()
    {
        isEmpty = true;

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            GameObject burn = (GameObject)Instantiate(fire, enemy.transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(enemy, 2f);
            Destroy(burn, 2f);
        }
        GameObject aura = (GameObject)Instantiate(crystalAura, transform.position + new Vector3(0,2,0), new Quaternion(0, 0, 0, 0));
        //GameObject env = (GameObject)Instantiate(crystalEnv, transform.position, new Quaternion(0, 0, 0, 0));

        aura.gameObject.name = "CrystalAura";
        //env.gameObject.name = "CrystalEnv";

        Destroy(aura, 5f);

        playerMining.getNextWeapon();

    }
}
