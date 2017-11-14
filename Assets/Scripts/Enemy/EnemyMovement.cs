using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    EnemyFOV sight;
    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        sight = GetComponentInChildren<EnemyFOV>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (sight.playerInSight)
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                anim.SetFloat("EnemyMove", 1.0f);
                nav.SetDestination(player.position);
                this.transform.LookAt(player);               
            }
            else
            {
                nav.enabled = false;
            }
        } 
    }

    public void ChangeNavSpeed(int amount)
    {
        nav.speed += amount;
    }
}