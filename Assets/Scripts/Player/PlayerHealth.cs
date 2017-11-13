using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;

    public Image healthSlider;
    public Text healthText;
    public AudioClip deathClip;
    public AudioClip hurtClip;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerBonus playerBonus;
    bool isDead = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerBonus = GetComponentInChildren<PlayerBonus>();
        currentHealth = startingHealth;

        // just be sure the slider and the text of health are ok
        healthSlider.transform.localScale = new Vector3((currentHealth / startingHealth), 1, 1);
        healthText.text = currentHealth + "/" + startingHealth;
    }


    void Update()
    {

    }


    public void TakeDamage(int amount)
    {
        if (isDead || playerBonus.bonusShieldInUse)
        {
            return;
        }
        
        if (currentHealth - amount <= 0 && !isDead)
        {
            // if life is under or equal to 0
            currentHealth = 0;
            Death();

        } else
        {
            // he takes some damage
            currentHealth -= amount;
            anim.SetTrigger("Take Damage");            

            playerAudio.clip = hurtClip;
            playerAudio.Play();
        }

        healthSlider.transform.localScale = new Vector3((currentHealth / startingHealth), 1, 1);
        healthText.text = currentHealth + "/" + startingHealth;
    }


    void Death()
    {
        // we say the player is dead
        isDead = true;

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
