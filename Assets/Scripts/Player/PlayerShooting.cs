using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    // timer to shoot
    public float timeBetweenBullet = 0.33f;
    
    // timer before to rest his arms
    public float timeToGoBackToIdle = 0.90f;

    // bullet shoot by MARVIN
    public GameObject[] proj = new GameObject[4];

    // player current weapon
    public int currentWeapon = 0;

    public GameObject gunRightArm;


    // audio sound for the laser
    AudioSource laserAudio;

    // general timer to know the time between two update
    float timer;

    // animator to control marvin
    Animator anim;

    // speed of the bullet
    public int bulletSpeed = 350;

    void Start () {
        anim = GetComponent<Animator>();
        laserAudio = GetComponent<AudioSource>();
	}
	
	void Update () {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > timeBetweenBullet)
        {
            // we put up his arm and play the shoot anim
            //anim.SetBool("Right Aim", true);
            ///anim.SetBool("Right Rapid Attack", true);

            Shoot();

            //anim.SetBool("Right Rapid Attack", false);
        }
        else if (timer > timeToGoBackToIdle)
        {          
            // we put back is right arm in the normal position
            anim.SetBool("Right Aim", false);           
        }
	}

    public void Shoot()
    {
        // we reset the timer
        timer = 0f;       

        anim.SetBool("Right Aim", true);
        anim.SetTrigger("Right Blast Attack");

        laserAudio.Play();     

        GameObject bullet = (GameObject)Instantiate(proj[currentWeapon], gunRightArm.transform.position, Quaternion.identity);
        bullet.gameObject.name = "Bullet";
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);              
    }
    
}
