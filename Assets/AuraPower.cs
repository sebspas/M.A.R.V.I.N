﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPower : MonoBehaviour {

    // genral timer for the aura
    float timer;

    // damage of the aura
    public int AuraDamage = 5;

    // timer between to aura attack
    public float timerBetweenTwoAttack = 1.0f;

    // list of all the enemyInRange
    IList<GameObject> enemyInRange = new List<GameObject>();

    // player bonus to know when to enable the damage
    PlayerBonus playerBonus;

    // thunder strike
    public GameObject thunder;

    private void Start()
    {
        playerBonus = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerBonus>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timerBetweenTwoAttack && playerBonus.bonusAuraInUse)
        {
            AttackAllInRange();
        }   
    }

    void AttackAllInRange()
    {
        // we reset the timer
        timer = 0;

        // we attack every enemy in range
        foreach (var enemy in enemyInRange)
        {
            // make the thunder touch the enemy
            GameObject thunderStrike = GameObject.Instantiate(thunder, enemy.transform);
            Destroy(thunderStrike, 1);

            // remove the damage to the enemy life
            enemy.GetComponent<EnemyHealth>().TakeDamage(AuraDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // we add the enemy to the list of enemy in range
            if (!enemyInRange.Contains(other.gameObject))
                enemyInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // we remove the enemy from the list of enemy in range
            enemyInRange.Remove(other.gameObject);
        }
    }
}
