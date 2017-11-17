using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    BossHealth bossHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        bossHealth = GetComponent<BossHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = 6;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        double distToPlayer = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - player.transform.position.x), 2)
            + Mathf.Pow((this.transform.position.z - player.transform.position.z), 2));
        if (bossHealth.GetCurrentHealth() > 0 && playerHealth.GetCurrentHealth() > 0 && distToPlayer > 13f)
        {
            nav.enabled = true;
            anim.SetFloat("EnemyMove", 1.0f);
            nav.SetDestination(player.position);
            
        }
        else
        {
            nav.enabled = false;
            anim.SetFloat("EnemyMove", 0.0f);
        }
        this.transform.LookAt(player);
    }

    public void ChangeNavSpeed(int amount)
    {
        nav.speed += amount;
    }
}