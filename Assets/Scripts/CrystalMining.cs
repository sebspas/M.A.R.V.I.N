using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // all the position to spawn ennemies on
    public GameObject spawnPointContainer;
    private GameObject[] spawnPoints;
    public GameObject[] listSpawnableEnnemies;

    // the crystal sphere collider
    SphereCollider sc;

    // the amont of crystal still to mine
    public float remainingCrystal = 100f;
    private float totalToMine;
    private float minnedCrystal = 0f;

    // spawning speed by wave
    public float spawningSpeed = 10f;

    // number of basic ennemie to spawn by wace
    int numberOfBasicEnnemies = 2;

    // number of distance ennemies to spawn by wave
    int numberOfDistanceEnnemies = 1;

    // timer for the spawning
    float timerSpawning = 0f;

    // timer to mine
    float miningSpeed;
    // The amont of cristal per timeBetweenCristal the player is mining
    float miningAmont;
    bool isEmpty;

    // progress bar for the crystal mining
    public Image crystalProgress;

    // public UI group to activate when we start mining 
    public GameObject uiCrystal;


    // Use this for initialization
    void Start () {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMining = player.GetComponent<PlayerMining>();

        miningAmont = playerMining.miningAmont;
        miningSpeed = playerMining.miningSpeed;
        totalToMine = remainingCrystal;

        // we add all the spawnpoints
        int children = spawnPointContainer.transform.childCount;
        spawnPoints = new GameObject[children]; // we init the tab of all the spawnPoints at the right size
        for (int i = 0; i < children; ++i)
            spawnPoints[i] = spawnPointContainer.transform.GetChild(i).gameObject;

        // Create the sphere collider, radius = 2, isTrigger = true (we can go through it)
        sc = gameObject.GetComponent<SphereCollider>() as SphereCollider;
        isEmpty = false;
    }
	
	// Update is called once per frame
	void Update () {    
        if (!isEmpty)
        {
            // we update the timer
            timer += Time.deltaTime;
           
            if (playerInRange)
            {
                timerSpawning += Time.deltaTime;
                if (timerSpawning > spawningSpeed)
                {
                    SpawnEnnemies();
                }          

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

    void SpawnEnnemies()
    {
        Debug.Log("Spawn Ennemie");
        // reset the timer
        timerSpawning = 0f;

        for (int i = 0; i < numberOfBasicEnnemies; i++)
        {
            int spawnPointPos = Random.Range(0, spawnPoints.Length-1);

            GameObject ennemie = (GameObject)Instantiate(listSpawnableEnnemies[0], spawnPoints[spawnPointPos].transform.position, new Quaternion(0, 0, 0, 0));
            ennemie.gameObject.name = "Ennemie_Basic_" + i;
        }

        for (int i = 0; i < numberOfDistanceEnnemies; i++)
        {
            int spawnPointPos = Random.Range(0, spawnPoints.Length);

            GameObject ennemie = (GameObject)Instantiate(listSpawnableEnnemies[1], spawnPoints[spawnPointPos].transform.position, new Quaternion(0, 0, 0, 0));
            ennemie.gameObject.name = "Ennemie_Distance_" + i;
        }

    }

    void Mine ()
    {
        uiCrystal.SetActive(true);

        if (timer > miningSpeed)
        {
            remainingCrystal -= miningAmont;
            minnedCrystal += miningAmont;
            timer = 0f;

            // make the progress bar move
            crystalProgress.transform.localScale = new Vector3((minnedCrystal / totalToMine), 1, 1);
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
