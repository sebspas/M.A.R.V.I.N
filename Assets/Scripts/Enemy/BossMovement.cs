using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    BossHealth bossHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    bool inFront;
    int layerMask;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        bossHealth = GetComponent<BossHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = 6;
        anim = GetComponent<Animator>();
        bool inFront = false;

        layerMask = 1 << 10;
        // This would cast rays only against colliders in layer 10.
        // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
    }

    void Update()
    {
        // vector between boss and player
        Vector3 direction = player.transform.position - transform.position;

        // distance between boss and player
        double distToPlayer = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - player.transform.position.x), 2)
            + Mathf.Pow((this.transform.position.z - player.transform.position.z), 2));

        // then we check if there is no obstacle to be able to see the player
        RaycastHit hit;


        // we use transform up to not cast the raycast from the feet
        // we use SphereCast to take care of the size of the bullet
        if (Physics.SphereCast(transform.position + transform.up, 1,
            direction.normalized, out hit, 50, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                inFront = true;
            }
            else
            {
                inFront = false;
            }
        }

        if ((bossHealth.GetCurrentHealth() > 0 && playerHealth.GetCurrentHealth() > 0) && (distToPlayer > 13f || !inFront))
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