﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10;

    public int numberOfAttacks = 2;
    protected int chooseAttack = 0;

    public float attackRange = 3.2f;

    protected Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyFOV sight;
    bool playerInRange;
    protected float timer;


    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        sight = GetComponentInChildren<EnemyFOV>();
        anim = GetComponent<Animator>();
    }


    protected void Update()
    {
        timer += Time.deltaTime;
        
        if (IsReadyToAttack())
        {
            anim.SetBool("PlayerInRange", true);
            Attack();
        } else
        {
            anim.SetBool("PlayerInRange", false);
        }       

        if (playerHealth.currentHealth <= 0)
        {
            // so it doesn't keep attacking it
            anim.SetBool("PlayerInRange", false);
            anim.SetTrigger("PlayerDead");
        }
    }

    protected virtual void Attack()
    {
       
        timer = 0f;
        anim.SetInteger("NumAttack", chooseAttack);
        chooseAttack = (++chooseAttack) % numberOfAttacks;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        
    }

    protected bool IsReadyToAttack()
    {
        return (timer >= timeBetweenAttacks && !enemyHealth.isEnemyDead() && IsInAttackRange());
    }

    protected bool IsInAttackRange()
    {
        if (!sight.playerInSight)
        {
            return false;
        }

        double distToPlayer = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - player.transform.position.x),2) 
            + Mathf.Pow((this.transform.position.z - player.transform.position.z),2));

        //Debug.Log(distToPlayer);
        return (distToPlayer <= attackRange);
    }
}
