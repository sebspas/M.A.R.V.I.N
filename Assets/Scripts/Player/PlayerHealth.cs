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


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            anim.SetTrigger("Take Damage");
            damaged = false;
        }
    }


    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        healthSlider.transform.localScale = new Vector3((currentHealth/startingHealth), 1, 1);
        //healthSlider.value = currentHealth;

        //playerAudio.Play();

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

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
