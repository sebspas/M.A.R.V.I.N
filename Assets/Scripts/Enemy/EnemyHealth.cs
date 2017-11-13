using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // starting health of the monster
    public int startingHealth = 30;
    public int currentHealth;

    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    // the player shooting script (to add the exp)
    PlayerShooting playerShooting;

    // amount of xp given when we kill the monster
    public int xpGiven = 2;

    // manager to apply the different effect
    Effect currentEffect;

    // animator to control animation
    Animator anim;

    // fire effect for this monster (to define in the editor)
    public GameObject fireEffect;

    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    // for effect and take damage
    EnemyMovement enemyMovement;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
        currentEffect = new Effect(this, enemyMovement);
    }


    void Update()
    {
        if (!isDead)
        {
            // calculate the effect to apply to the entity
            currentEffect.applyEffects();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
            //enemyAudio.Play();
            anim.SetTrigger("EnemyHurt");
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(BulletScript bullet)
    {
        if (isDead)
            return;

        int amount = currentEffect.getHurt(bullet);

        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= amount;
            //enemyAudio.Play();
            anim.SetTrigger("EnemyHurt");
        }

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public bool isEnemyDead()
    {
        return isDead;
    }

    void Death()
    {
        anim.SetBool("EnemyDead", true);
        anim.SetBool("PlayerInRange", false);
        isDead = true;

        // Turn the capsule collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        // we give the amount of xp to the player max energy
        playerShooting.energyMax += xpGiven;
        // we also give this energy to the current amount of energy of the player
        playerShooting.currentEnergy += xpGiven;

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();

        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        //GetComponent<Rigidbody>().isKinematic = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 second destory the enemy.
        Destroy(gameObject, 2f);
    }
}
