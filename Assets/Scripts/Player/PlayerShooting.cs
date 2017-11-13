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
    private float energyRegenTime = 0.5f;

    // energy regenValue (in percent of the max energy)
    private float energyRegen = 0.05f;

    // startingEnergy (1 basic shoot cost 1, ice 2, fire 3, and earth 3)
    public float energyMax = 20;

    // currentEnergyLevel
    public float currentEnergy = 0;

    // slider for the bullet energy/xp
    public Image energySlider;
    public Text energyText;

    // point where the bullet come from
    public GameObject gunRightArm;

    // audio laser sound
    public AudioClip laser;

    // audio source for the laser
    AudioSource laserAudio;

    // To detect if a bonus is in use
    PlayerBonus playerBonus;

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
        playerBonus = GetComponentInChildren<PlayerBonus>();
    }
	
	void Update () {
        // we update the two timer
        timer += Time.deltaTime;
        timerEnergy += Time.deltaTime;

        // if we clicck and we have enough energy for the selected bullet
        bool isFirerateOk = (timer > timeBetweenBullet && !playerBonus.bonusBoostInUse) || (timer > timeBetweenBullet / 2 && playerBonus.bonusBoostInUse); 
        if (Input.GetButton("Fire1") && isFirerateOk && currentEnergy > proj[currentWeapon].GetComponent<BulletScript>().energyCost)
        {
            // then we shoot
            Shoot();
        }
        else if (timer > timeToGoBackToIdle)
        {          
            // we put back is right arm in the normal position
            anim.SetBool("Right Aim", false);           
        }

        // we increase the energy if neeeded
        if (timerEnergy > energyRegenTime)
        {
            // we reset the timer
            timerEnergy = 0;

            // if we are not using bonus 3
            if (!playerBonus.bonusBoostInUse) 
            {
                // we regen the energy
                if (currentEnergy + (energyMax*energyRegen) <= energyMax)
                {
                    currentEnergy += (energyMax * energyRegen);
                }
                else
                {
                    currentEnergy = energyMax;
                }
            }

            // we update the energy slider
            energySlider.transform.localScale = new Vector3((currentEnergy / energyMax), 1, 1);
            energyText.text = currentEnergy.ToString("#.0") + "/" + energyMax;
        
            //Debug.Log(currentEnergy + "/" + energyMax);
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
        laserAudio.clip = laser;
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
