using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour {

    // general timer to know the time between two update
    float timer;

    // timer to mine
    public float miningSpeed = 0.5f;

    // The amount of cristal per timeBetweenCristal the player is mining
    public float miningAmont = 0.5f;

    // The total amount of crystal mined so far
    public float totalMined;

    // boolean to know if the player is mining the cristal or recharging the portal
    public bool isMining; 
    public bool isCharging;



	// Use this for initialization
	void Start () {
        isMining = false;
        isCharging = false;
        timer = 0f;
        totalMined = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        // we update the timer
        timer += Time.deltaTime;

        if (isMining)
        {
            if (timer>miningSpeed)
            {
                totalMined += miningAmont;
                timer = 0f;
            }
        } else
        {
            timer = 0f;
        }

    }


}
