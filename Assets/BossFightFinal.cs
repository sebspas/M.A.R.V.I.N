using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightFinal : MonoBehaviour
{
    public GameObject[] wall;
    public GameObject[] effect;
    public GameObject effectAOE;
    public GameObject spawnPoint;
    public GameObject boss;

    bool begin;
    float timer;


    // Use this for initialization
    void Start()
    {
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && timer < Time.time)
        {
            //print("instanciate boss");
            GameObject Instanceboss = (GameObject)Instantiate(boss, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
            Instanceboss.SetActive(true);

            begin = false;
            timer = 0;
        }
    }

    public void Begin()
    {
        begin = true;

        foreach( GameObject w in wall)
        {
            w.SetActive(true);
        }

        foreach(GameObject e in effect)
        {
            e.SetActive(true);
        }

        timer = Time.time + 8f;
    }

    public void LaunchAOE()
    {
        effectAOE.SetActive(true);
    }
    public void StopAOE()
    {
        effectAOE.SetActive(false);
    }
    public void DestroyWall()
    {
        foreach (GameObject w in wall)
        {
            w.SetActive(false);
        }

        foreach (GameObject e in effect)
        {
            e.SetActive(false);
        }

        foreach (GameObject w in wall)
        {
            Destroy(w, 2f);
        }
    }
}
