using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    // effect show when the boss disappear
    public GameObject disappearEffect;

    // global UI Element for the boss
    GameObject UIBoss;

    // Image use to show the boss life
    Image healthSlider;

    // the player shooting script (to add the exp)
    PlayerShooting playerShooting;

    // amount of xp given when we kill the monster
    public int xpGiven = 2;

    // animator to control animation
    Animator anim;

    bool isSinking;

    // for effect and take damage
    EnemyMovement bossMovement;

    BossFight scriptZoneBoss;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hurtSound = GetComponent<AudioSource>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        bossMovement = GetComponentInParent<EnemyMovement>();
        currentEffect = new Effect(this, bossMovement);
        InitHealth();
        AppearOrDesappear();

        // UI boss management
        UIBoss = GameObject.FindGameObjectWithTag("BossUI");
        GameObject healthSliderObject = GameObject.FindGameObjectWithTag("BossUILife");
        if (healthSliderObject != null) healthSlider = healthSliderObject.GetComponent<Image>();
        SliderUpdate();
        // make the HUD Appear
        UIBoss.GetComponent<Animator>().SetTrigger("Active");       
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
        SliderUpdate();
    }

    // Override the Death Function to define the death behavior
    public override void Death()
    {
        SliderUpdate();
        anim.SetBool("BossDead", true);
        anim.SetBool("PlayerInRange", false);

        UIBoss.GetComponent<Animator>().SetTrigger("Disable");

        // we give the amount of xp to the player max energy
        playerShooting.energyMax += xpGiven;
        // we also give this energy to the current amount of energy of the player
        playerShooting.currentEnergy += xpGiven;

        GetComponent<AudioSource>().clip = deathClip;
        GetComponent<AudioSource>().Play();

        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        AppearOrDesappear();

        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.Healed(1000);

        // After 2 second destory the boss.
        Destroy(gameObject, 2f);     
    }


    private void SliderUpdate()
    {
        // just be sure the slider and the text of health are ok
        healthSlider.transform.localScale = new Vector3((currentHealth / maxHealth), 1, 1);
    }

    public void AppearOrDesappear()
    {
        GameObject effectAnim = GameObject.Instantiate(disappearEffect, this.transform.position, Quaternion.identity) as GameObject;
        effectAnim.transform.parent = this.gameObject.transform;
        effectAnim.SetActive(true);
    }
}
