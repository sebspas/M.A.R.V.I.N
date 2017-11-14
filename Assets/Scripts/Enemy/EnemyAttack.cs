using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10;

    public int numberOfAttacks = 2;
    private int chooseAttack = 0;

    public float attackRange = 3.2f;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyFOV sight;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        sight = GetComponentInChildren<EnemyFOV>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeBetweenAttacks && !enemyHealth.isEnemyDead() && IsInAttackRange() && sight.playerInSight)
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


    protected void Attack()
    {
       
        timer = 0f;
        anim.SetInteger("NumAttack", chooseAttack);
        chooseAttack = (++chooseAttack) % numberOfAttacks;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        
    }

    bool IsInAttackRange()
    {
        double distToPlayer = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - player.transform.position.x),2) 
            + Mathf.Pow((this.transform.position.z - player.transform.position.z),2));

        //Debug.Log(distToPlayer);
        return (distToPlayer <= attackRange);
    }
}
