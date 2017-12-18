using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour, IAttack
{
    // time between two boss attack
    public float timeBetweenAttacks = 0.5f;

    // normal bpss attack damage
    public int attackDamage = 40;

    // AOE damage
    public int AOEattackDamage = 50;

    // boss attack range
    public float attackRange = 15f;

    // boss AOE
    public GameObject effectAOE;

    // bullet shoot by boss
    public GameObject bossProj;

    // point where the bullet come from
    public GameObject staff;

    // speed of the bullet
    public int bossBulletSpeed = 300;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    BossHealth bossHealth;
    bool playerInRange;
    protected float timer;
    EnemyFOV sight;

    // For the sounds of the AOE
    public AudioClip bossAOEclip;

    AudioSource audio1;
    // counter for pattern attacks
    int patternCount = 0;
    // Si boss faible PV changement de pattern
    bool venerePattern = false;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        bossHealth = GetComponent<BossHealth>();
        anim = GetComponent<Animator>();
        sight = GetComponentInChildren<EnemyFOV>();
        audio1 = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        timer += Time.deltaTime;

        if(bossHealth.GetCurrentHealth() <= 400)
        {
            venerePattern = true;
        }

        if (IsReadyToAttack())
        {
            StopAOE();
            anim.SetBool("PlayerInRange", true);
            PatternAttack();
        }
        else
        {
            anim.SetBool("PlayerInRange", false);
        }

        if (playerHealth.IsDead())
        {
            // so it doesn't keep attacking it
            anim.SetBool("PlayerInRange", false);
            anim.SetTrigger("PlayerDead");
        }
    }

    protected void PatternAttack()
    {
        
        if (!venerePattern)
        {
            if (patternCount % 12 < 11)
            {
                Attack();
            }
            else
            {
                AOEAttack();
            }      
        }
        else
        {
            if (patternCount % 7 < 6)
            {
                Attack();
            }
            else
            {
                AOEAttack();
            }
        }
        patternCount += 1;
    }

    void AOEAttack()
    {
        timer = 0f;

        //anim.SetInteger("NumAttack", 1);
        anim.SetTrigger("Numun");

        LaunchAOE();

        Invoke("AOEDamage", 1.2f);      
    }

    // inflict damage to the player for the AOE
    void AOEDamage()
    {
        playerHealth.TakeDamage(AOEattackDamage);
    }

    // override parent method Attack()
    void Attack()
    {
        timer = 0f;

        //anim.SetInteger("NumAttack", 0);
        anim.SetTrigger("Numdeux");
    }

   bool IsReadyToAttack()
    {
        return (timer >= timeBetweenAttacks && !bossHealth.IsDead() && IsInAttackRange());
    }

    public void LaunchBossBullet()
    {
        // we launch the bullet
        GameObject bullet = (GameObject)Instantiate(bossProj, staff.transform.position - new Vector3(0f,1.5f,0f), Quaternion.identity);
        bullet.gameObject.name = "Bullet";
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bossBulletSpeed);
    }

    public void LaunchAOE()
    {
        audio1.PlayOneShot(bossAOEclip,1.2f);
        effectAOE.SetActive(true);
    }

    public void StopAOE()
    {
        effectAOE.SetActive(false);
    }

    public bool IsInAttackRange()
    {
        if (!sight.playerInSight)
        {
            return false;
        }

        float distToPlayer = Mathf.Pow((this.transform.position.x - player.transform.position.x), 2)
            + Mathf.Pow((this.transform.position.z - player.transform.position.z), 2);

        return (distToPlayer <= Mathf.Pow(attackRange, 2)) && sight.IsClearSight(player);
    }
}
