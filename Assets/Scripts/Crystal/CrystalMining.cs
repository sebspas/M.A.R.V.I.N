using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalMining : Spawner {

    // general timer to know the time between two update
    float timerMining = 0f;

    bool playerInRange;
    bool beginMining;
    GameObject player;
    PlayerMining playerMining;

    public GameObject flyingCrystal;

    // End of crystal animations:
    public GameObject crystalAura;
    //public GameObject crystalEnv;

    // list of all the enemies when the crystal is ended
    public GameObject[] allEnemies;
    public GameObject fire;

    // all the position to spawn ennemies on
    //public GameObject spawnPointContainer;
    public GameObject[] spawnPoints;
    public GameObject[] listSpawnableEnnemies;

    // the crystal sphere collider
    SphereCollider sc;

    // the amont of crystal still to mine
    public float remainingCrystal = 100f;
    private float totalToMine;
    private float minnedCrystal = 0f;

    // timer to mine
    float miningSpeed;
    // The amont of cristal per timeBetweenCristal the player is mining
    float miningAmont;
    bool isEmpty;

    // progress bar for the crystal mining
    GameObject crystalProgress;

    // public UI group to activate when we start mining 
    GameObject uiCrystal;

    // Use this for initialization
    void Start () {
        playerInRange = false;
        beginMining = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMining = player.GetComponent<PlayerMining>();

        uiCrystal = gameObject.transform.GetChild(0).gameObject;
        uiCrystal.SetActive(false);
        
        for (int i=0; i<uiCrystal.transform.childCount; i++)
        {
            if (uiCrystal.transform.GetChild(i).name == "FillCrystal")
            { 
                crystalProgress = uiCrystal.transform.GetChild(i).gameObject;
            }
        }

        miningAmont = playerMining.miningAmont;
        miningSpeed = playerMining.miningSpeed;
        totalToMine = remainingCrystal;

        // Create the sphere collider, radius = 2, isTrigger = true (we can go through it)
        sc = gameObject.GetComponent<SphereCollider>() as SphereCollider;
        isEmpty = false;

        // setup the number of ennemie
        numberOfBasicEnnemies = 2;
        numberOfRangeEnnemies = 2;
        numberOfTankEnnemies = 1;
        maxWave = 99;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isEmpty)
        {
            // we update the timer
            timerMining += Time.deltaTime;

            // The pkayer must press 'E' in order to begin mining
            if (playerInRange && Input.GetKeyDown("space"))
            {
                beginMining = true;
            }

            // We begin the mining and the ennemies spawn
            if (playerInRange && beginMining)
            {                
                playerMining.isMining = true;
                active = true;
                if (timerMining > miningSpeed)
                {
                    Mine();
                }            
            }
            else
            {
                playerMining.isMining = false;
                active = false;
            }

            // if the spawner is activate it will spawn monster, otherwise it will not do anything
            updateSpawner(Time.deltaTime);

            if (remainingCrystal <= 0)
            {
                playerMining.isMining = false;
                beginMining = false;
                active = false;
                endOfCrystal();

                playerMining.endOfCrystal(transform);
            }
        }
	}

    void OnTriggerEnter(Collider sc)
    {
        if (sc.gameObject.tag == "Player")
        {
            playerInRange = true;          
        }
    }

    void OnTriggerExit(Collider sc)
    {
        if (sc.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    protected override void SpawnWave()
    {
        SpawnEnnemieAtRandom(listSpawnableEnnemies[0], spawnPoints, numberOfBasicEnnemies);          
        SpawnEnnemieAtRandom(listSpawnableEnnemies[1], spawnPoints, numberOfRangeEnnemies);

        if(listSpawnableEnnemies.Length > 2)
        {
            SpawnEnnemieAtRandom(listSpawnableEnnemies[2], spawnPoints, numberOfTankEnnemies);
        }
    }

    void Mine ()
    {
        uiCrystal.SetActive(true);

        if (timerMining > miningSpeed)
        {
            remainingCrystal -= miningAmont;
            minnedCrystal += miningAmont;
            timerMining = 0f;

            // make the progress bar move
            float newScale = remainingCrystal / totalToMine;
            crystalProgress.transform.localScale = new Vector3(newScale, 1, 1);
        }

        // we launch the floating crystal
        GameObject floatingCrystal = (GameObject)Instantiate(flyingCrystal, transform.position, new Quaternion(0,0,0,0));
        floatingCrystal.gameObject.name = "FloatingCrystal";
        
    }

    void endOfCrystal()
    {
        isEmpty = true;

        uiCrystal.SetActive(false);

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            GameObject burn = (GameObject)Instantiate(fire, enemy.transform.position, new Quaternion(0, 0, 0, 0));
            // we set the enemy as parent so the fire moves with the enemy
            burn.transform.parent = enemy.transform;
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
