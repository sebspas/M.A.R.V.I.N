using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public float timeBetweenBullet = 0.15f;
    public float timeToGoBackToIdle = 0.90f;

    float timer;
    Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > timeBetweenBullet)
        {
            Shoot();
        } else if (timer > timeToGoBackToIdle)
        {          
            // we put back is right arm in the normal position
            anim.SetBool("Right Aim", false);
        }
	}

    public void Shoot()
    {
        // we reset the timer
        timer = 0f;

        // we put up his arm and play the shoot anim
        anim.SetBool("Right Aim", true);

        anim.SetTrigger("Right Blast Attack");
    }
}
