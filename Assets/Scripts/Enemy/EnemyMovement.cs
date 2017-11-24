using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    Health enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    EnemyFOV sight;
    Animator anim;
    IAttack attack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<Health>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = 6;
        sight = GetComponentInChildren<EnemyFOV>();
        anim = GetComponent<Animator>();
        attack = GetComponent<IAttack>();
    }

    void Update()
    {
        if (sight.playerInSight)
        {
            if (enemyHealth.GetCurrentHealth() > 0 && playerHealth.GetCurrentHealth() > 0)
            {
                // lookt at the player
                transform.LookAt(player);

                if (!attack.IsInAttackRange())
                {
                    anim.SetFloat("EnemyMove", 1.0f);
                    nav.enabled = true;
                    nav.SetDestination(player.position);
                }
                else
                {
                    nav.enabled = false;
                    anim.SetFloat("EnemyMove", 0.0f);
                }
            }            
        } else
        {
            anim.SetFloat("EnemyMove", 0.0f);
        }
    }

    public void ChangeNavSpeed(int amount)
    {
        nav.speed += amount;
    }
}