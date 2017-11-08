﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // starting health of the monster
    public int startingHealth = 30;
    public int currentHealth;

    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    // the player shooting script (to add the exp)
    PlayerShooting playerShooting;

    // amount of xp given when we kill the monster
    public int xpGiven = 2;

    // manager to apply the different effect
    Effect currentEffect;

    // animator to control animation
    public Animator anim;

    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();

        EnemyMovement enemyMovement = GetComponentInParent<EnemyMovement>();
        currentEffect = new Effect(this, enemyMovement);
    }


    void Update()
    {
        // calculate the effect to apply to the entity
        currentEffect.applyEffects();

        if (isSinking)
        {
            // Move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(BulletScript bullet)
    {
        if (isDead)
            return;

        //enemyAudio.Play();
        currentEffect.getHurt(bullet);


        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("EnemyHurt");
        }
    }


    void Death()
    {
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        anim.SetBool("EnemyDead", true);

        // we give the amount of xp to the player max energy
        playerShooting.energyMax += xpGiven;
        // we also give this energy to the current amount of energy of the player
        playerShooting.currentEnergy += xpGiven;

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }


    public void StartSinking()
    {
        print("start sinking");
        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should now sink.
        //isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 1 second destory the enemy.
        Destroy(gameObject, 1f);
    }
}
