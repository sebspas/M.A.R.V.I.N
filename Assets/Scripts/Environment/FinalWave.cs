using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWave : Spawner
{
    // All different Zone SpawnPoints
    public GameObject spawnIce;
    public GameObject spawnDesert;
    public GameObject spawnForest;

    // all the different ennemie for each spawn
    public GameObject[] ennemiesIce;
    public GameObject[] ennemiesDesert;
    public GameObject[] ennemiesForest;

    // The wall to activate
    public GameObject walls;

    // the player
    private PlayerShooting playerShooting;

    // to trigger once only
    private bool canBegin;

    // Use this for initialization
    void Start()
    {
        // we get the playerWeapon component, to know if the player got all the weapons
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        canBegin = true;
    }

    private void Update()
    {
        updateSpawner(Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // we check if the different power
        if (other.gameObject.tag == "Player" && canBegin)
        {
            if (playerShooting.playerGotAllWeapon())
            {
                InitFinalWave();
                canBegin = false;
            }
        }
    }

    public void InitFinalWave()
    {
        // we activate the wall
        walls.SetActive(true);

        // we launched the spawn of the ennemies
        active = true;
    }

    protected override void SpawnWave()
    {
        // spawnIce
        SpawnEnnemie(ennemiesIce[0], spawnIce, numberOfBasicEnnemies);
        SpawnEnnemie(ennemiesIce[1], spawnIce, numberOfRangeEnnemies);

        // spawnForest
        SpawnEnnemie(ennemiesForest[0], spawnForest, numberOfBasicEnnemies);
        SpawnEnnemie(ennemiesForest[1], spawnForest, numberOfRangeEnnemies);
        SpawnEnnemie(ennemiesForest[2], spawnForest, numberOfTankEnnemies);

        // spawnDesert
        SpawnEnnemie(ennemiesDesert[0], spawnDesert, numberOfBasicEnnemies);
        SpawnEnnemie(ennemiesDesert[1], spawnDesert, numberOfRangeEnnemies);
        SpawnEnnemie(ennemiesDesert[2], spawnDesert, numberOfTankEnnemies);
    }

    protected override void EndSpawn()
    {
        BossFightFinal bossScript = GameObject.FindGameObjectWithTag("FinalGameplay").GetComponent<BossFightFinal>();
        bossScript.Begin();
        // TODO
        // make the boss spawn HERE
    }

    public void DestroyWall()
    {
        updateSpawner(Time.deltaTime);
    }
}
