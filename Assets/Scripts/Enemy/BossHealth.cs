using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject disappearEffect;

    // global UI Element for the boss
    GameObject UIBoss;

    // Image use to show the boss life
    Image healthSlider;

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

    BossFight scriptZoneBoss;

    bool final;

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

        final = false;
        // UI boss management
        UIBoss = GameObject.FindGameObjectWithTag("BossUI");
        GameObject healthSliderObject = GameObject.FindGameObjectWithTag("BossUILife");
        if (healthSliderObject != null) healthSlider = healthSliderObject.GetComponent<Image>();
        SliderUpdate();
        // make the HUD Appear
        UIBoss.GetComponent<Animator>().SetTrigger("Active");

        // The zone depends on the number of weapon
        int nbWeapon = playerShooting.GetMaxWeapon();
        // Recupere la zone du boss pour pouvoir lancer l'effet de l'AOE
        switch (nbWeapon)
        {
            case 2:
                scriptZoneBoss = GameObject.FindGameObjectWithTag("IceGameplay").GetComponent<BossFight>();
                break;
            case 3:
                scriptZoneBoss = GameObject.FindGameObjectWithTag("FireGameplay").GetComponent<BossFight>();
                break;
            case 4:
                scriptZoneBoss = GameObject.FindGameObjectWithTag("ForestGameplay").GetComponent<BossFight>();
                break;
            case 5:
                scriptZoneBoss = GameObject.FindGameObjectWithTag("FinalGameplay").GetComponent<BossFight>();
                final = true;
                break;
        }
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

        if (!final)
            scriptZoneBoss.DestroyWall();

        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.Healed(1000);

        if (playerShooting.playerGotAllWeapon())
        {
            // if we are at the end and we kill the boss
            // then we make the endgame screen appear
            GameObject.FindGameObjectWithTag("HUDEndGame").GetComponent<Animator>().SetTrigger("EndGame");

            // we stop the game
            isPaused = true;
            Time.timeScale = 0;
        }
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
