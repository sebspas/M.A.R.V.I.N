using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : Health
{
    public Image healthSlider;
    public Text healthText;

    public AudioClip deathClip;
    public AudioClip hurtClip;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerBonus playerBonus;

    void Awake()
    {       
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerBonus = GetComponentInChildren<PlayerBonus>();
        InitHealth();
        SliderUpdate();
    }

    public void TakeDamage(int amount)
    {
        if (playerBonus.bonusShieldInUse)
            return;

        // remove the damage and launch all the needed function
        Damaged(amount);
    }

    private void SliderUpdate()
    {
        // just be sure the slider and the text of health are ok
        healthSlider.transform.localScale = new Vector3((currentHealth / maxHealth), 1, 1);
        healthText.text = currentHealth + "/" + maxHealth;
    }


    public override void HurtAnim()
    {
        anim.SetTrigger("Take Damage");

        playerAudio.clip = hurtClip;
        playerAudio.Play();
        SliderUpdate();
    }

    public override void HealingAnim()
    {
        SliderUpdate();
    }

    public override void Death()
    {
        SliderUpdate();
        // play the anim
        anim.SetTrigger("Die");

        // play the corresponding sound to the death
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // we stop the ability to shoot or to move
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
