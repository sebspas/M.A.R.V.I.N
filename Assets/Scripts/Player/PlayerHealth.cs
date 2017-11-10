using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;

    public Image healthSlider;
    public AudioClip deathClip;
    public AudioClip hurtClip;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerBonus playerBonus;
    bool isDead;
    bool damaged = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerBonus = GetComponentInChildren<PlayerBonus>();
        currentHealth = startingHealth;
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
        
        if (currentHealth - amount < 0)
        {
            currentHealth = 0;
            
        } else
        {
            currentHealth -= amount;
            anim.SetTrigger("Take Damage");            

            playerAudio.clip = hurtClip;
            playerAudio.Play();
        }

        healthSlider.transform.localScale = new Vector3((currentHealth / startingHealth), 1, 1);

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        print("dead");
        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
