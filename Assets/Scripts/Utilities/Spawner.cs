using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // bool to know if the final wave is launch
    protected bool active = false;

    // the number of wave
    protected int wave = 1;

    // number max of wave
    protected int maxWave = 10;

    // general timer to know the time between two update
    protected float timerSpawn = 0f;

    // time between two wave
    protected float spawningSpeed = 5.0f;

    // number of basic ennemies
    protected int numberOfBasicEnnemies = 1;

    // number of ranged ennemies
    protected int numberOfRangeEnnemies = 1;

    // number of tank
    protected int numberOfTankEnnemies = 0;

    protected void SpawnEnnemie(GameObject ennemie, GameObject spawnPoint, int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject ennemy = (GameObject)Instantiate(ennemie, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
            ennemy.gameObject.name = "Ennemy_" + i;
            ennemy.GetComponentInChildren<EnemyFOV>().disabled = true;
            ennemy.GetComponentInChildren<EnemyFOV>().playerInSight = true;
        }
    }

    protected void SpawnEnnemieAtRandom(GameObject ennemie, GameObject[] spawnPoints, int number)
    {
        for (int i = 0; i < number; i++)
        {
            int spawnPointPos = Random.Range(0, spawnPoints.Length - 1);
            GameObject ennemy = (GameObject)Instantiate(ennemie, spawnPoints[spawnPointPos].transform.position, new Quaternion(0, 0, 0, 0));
            ennemy.gameObject.name = "Ennemy_" + i;
            ennemy.GetComponentInChildren<EnemyFOV>().disabled = true;
            ennemy.GetComponentInChildren<EnemyFOV>().playerInSight = true;
        }
    }

    protected void NextWave() {
        // we inc the number of wave
        wave++;

        // we inc the time before the next wave
        spawningSpeed += wave * 2f;

        // reset the timer
        timerSpawn = 0f;

        // call the method define in the child to spawn all the ennemies of this wave
        SpawnWave();

        // we inc the number of basic
        numberOfBasicEnnemies += wave;

        // we inc the number of ranged
        numberOfRangeEnnemies += (wave /2);

        // we inc the number of tank
        numberOfTankEnnemies += (wave/3);

        // check if its over
        if (wave >= maxWave)
        {
            // we finish by calling the finish function define in the child
            EndSpawn();

            // we disable the FinalWave
            active = false;
        }

    }

    protected void updateSpawner(float deltaTime)
    {
        if (active)
        {
            // we update the timer
            timerSpawn += deltaTime;

            if (timerSpawn >= spawningSpeed && wave <= maxWave)
            {
                NextWave();
            }
        }
    }

    protected virtual void SpawnWave() { }

    protected virtual void EndSpawn() { }
}
