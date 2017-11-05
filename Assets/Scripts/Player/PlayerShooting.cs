using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

    // timer to shoot
    public float timeBetweenBullet = 0.33f;
    
    // timer before to rest his arms
    public float timeToGoBackToIdle = 0.90f;

    // bullet shoot by MARVIN
    public GameObject[] proj = new GameObject[4];

    // player current weapon
    public int currentWeapon = 0;

    // energy regenerationRate
    public float energyRegenTime = 2.0f;

    // energy regenValue
    public int energyRegen = 1;

    // startingEnergy (1 basic shoot cost 1, ice 2, fire 3, and earth 3)
    public float energyMax = 20;

    // currentEnergyLevel
    public float currentEnergy = 0;

    // slider for the bullet energy/xp
    public Image energySlider;

    // point where the bullet come from
    public GameObject gunRightArm;


    // audio sound for the laser
    AudioSource laserAudio;

    // general timer to know the time between two update
    float timer;

    // timer for the energy
    float timerEnergy;

    // animator to control marvin
    Animator anim;

    // speed of the bullet
    public int bulletSpeed = 350;

    void Start () {
        anim = GetComponent<Animator>();
        laserAudio = GetComponent<AudioSource>();
	}
	
	void Update () {
        // we update the two timer
        timer += Time.deltaTime;
        timerEnergy += Time.deltaTime;

        // if we clicck and we have enough energy for the selected bullet
        if (Input.GetButton("Fire1") && timer > timeBetweenBullet && currentEnergy > proj[currentWeapon].GetComponent<BulletScript>().energyCost)
        {
            // then we shoot
            Shoot();
        }
        else if (timer > timeToGoBackToIdle)
        {          
            // we put back is right arm in the normal position
            anim.SetBool("Right Aim", false);           
        }

        // we increase the nergy if neeeded
        if (timerEnergy > energyRegenTime)
        {
            // we reset the timer
            timerEnergy = 0;

            // we regen the energy
            if (currentEnergy+energyRegen <= energyMax)
            {
                currentEnergy += energyRegen;
            } else
            {
                currentEnergy = energyMax;
            }

            // we update the energy slider
            energySlider.transform.localScale = new Vector3((currentEnergy / energyMax), 1, 1);

            Debug.Log(currentEnergy + "/" + energyMax);
        }

        
    }

    public void Shoot()
    {

        // we reset the timer
        timer = 0f;       

        // we play the animations
        anim.SetBool("Right Aim", true);
        anim.SetTrigger("Right Blast Attack");

        // the sound corresponding to the nergy
        laserAudio.Play();     

        // we launch the bullet
        GameObject bullet = (GameObject)Instantiate(proj[currentWeapon], gunRightArm.transform.position, Quaternion.identity);
        bullet.gameObject.name = "Bullet";
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

        // we deduce the cost in energy from the current energy
        currentEnergy -= bullet.GetComponent<BulletScript>().energyCost;

        // we update the energy slider
        energySlider.transform.localScale = new Vector3((currentEnergy / energyMax), 1, 1);
    }
    
}
