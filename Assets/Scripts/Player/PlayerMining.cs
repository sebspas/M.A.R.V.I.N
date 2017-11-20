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

    // The phase of game
    public int phase;

    // boolean to know if the player is mining the cristal or recharging the portal
    public bool isMining; 
    public bool isCharging;

    GameObject weaponUI;
    PlayerWeapon playerWeapon;


	// Use this for initialization
	void Start () {
        isMining = false;
        isCharging = false;
        timer = 0f;
        totalMined = 0f;
        phase = 1;

        weaponUI = GameObject.FindGameObjectWithTag("WeaponUI");
        playerWeapon = weaponUI.GetComponent<PlayerWeapon>();

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

    public void getNextWeapon()
    {
        playerWeapon.addWeapon();
    }

    public void endOfCrystal(Transform posCrystal)
    {
        Vector3 posBoss = posCrystal.position;
        BossFight bossScript;

        switch (phase)
        {
            case 1: // Ice phase
                // Find and launch the boss script linked to the icegameplay
                GameObject iceGameplay = GameObject.FindGameObjectWithTag("IceGameplay");
                bossScript = iceGameplay.GetComponent<BossFight>();
                bossScript.Begin();
                break;

            case 2: // fire phase
                // Find and launch the boss script linked to the firegameplay
                GameObject fireGameplay = GameObject.FindGameObjectWithTag("FireGameplay");
                bossScript = fireGameplay.GetComponent<BossFight>();
                bossScript.Begin();
                break;

            case 3: // Earth phase
                // Find and launch the boss script linked to the earthgameplay
                GameObject forestGameplay = GameObject.FindGameObjectWithTag("ForestGameplay");
                bossScript = forestGameplay.GetComponent<BossFight>();
                bossScript.Begin();
                break;

            default:
                break;
        }

        phase++;
    }
}
