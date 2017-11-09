using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    public UnityEngine.AI.NavMeshAgent nav;
    Animator anim;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0  && playerHealth.currentHealth > 0 )
        {
            nav.SetDestination(player.position);
            this.transform.LookAt(player);
        }
        else
        {
            nav.enabled = false;
            anim.SetTrigger("PlayerDead");
        }
    }
}
