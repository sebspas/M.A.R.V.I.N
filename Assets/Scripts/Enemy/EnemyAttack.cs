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
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !enemyHealth.isEnemyDead())
        {
            
            playerInRange = true;            
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && !enemyHealth.isEnemyDead())
        {
            playerInRange = false;
            
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && IsInAttackRange())
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


    void Attack()
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
        Debug.Log(this.transform.position.x- player.transform.position.x);
        return (this.transform.position.x - player.transform.position.x <= attackRange);
    }
}
