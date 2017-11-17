using UnityEngine;

public class BossHealth : Health
{
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject disappearEffect;

    // the player shooting script (to add the exp)
    PlayerShooting playerShooting;

    // amount of xp given when we kill the monster
    public int xpGiven = 2;

    // manager to apply the different effect
    EffectBoss currentEffect;

    // animator to control animation
    Animator anim;

    // fire effect for this monster (to define in the editor)
    public GameObject fireEffect;

    AudioSource bossAudio;
    bool isSinking;

    // for effect and take damage
    BossMovement bossMovement;

    GameObject boss;

    void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        anim = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        bossMovement = GetComponentInParent<BossMovement>();
        currentEffect = new EffectBoss(this, bossMovement);
        InitHealth();
        AppearOrDesappear();
    }


    void Update()
    {
        if (!isDead)
        {
            // calculate the effect to apply to the entity
            currentEffect.applyEffects();
        }
        else
        {
            this.transform.localScale -= new Vector3(0.0035f, 0.0035f, 0.0035f);
        }
    }

    public void TakeDamage(int damage)
    {
        // we applied the damage to the entity
        Damaged(damage);
    }

    public void TakeDamage(BulletScript bullet)
    {
        if (isDead)
            return;

        int damage = currentEffect.getHurt(bullet);

        // we applied the damage to the entity
        Damaged(damage);
    }

    //(nothing with the boss)
    public override void HurtAnim()
    {
    }

    // Override the Death Function to define the death behavior
    public override void Death()
    {
        anim.SetBool("BossDead", true);
        anim.SetBool("PlayerInRange", false);

        // we give the amount of xp to the player max energy
        playerShooting.energyMax += xpGiven;
        // we also give this energy to the current amount of energy of the player
        playerShooting.currentEnergy += xpGiven;

        bossAudio.clip = deathClip;
        bossAudio.Play();

        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the boss).
        //GetComponent<Rigidbody>().isKinematic = true;

        // Increase the score by the boss's score value.
        //ScoreManager.score += scoreValue;

        // After 2 second destory the boss.
        Destroy(gameObject, 2f);

        AppearOrDesappear();

        BossFight1 scriptBoss1 = GameObject.FindGameObjectWithTag("IceGameplay").GetComponent<BossFight1>();
        scriptBoss1.DestroyWall();
    }

    public void AppearOrDesappear()
    {
        GameObject effectAnim = GameObject.Instantiate(disappearEffect, this.transform.position, Quaternion.identity) as GameObject;
        effectAnim.transform.parent = this.gameObject.transform;
        effectAnim.SetActive(true);
    }
}
