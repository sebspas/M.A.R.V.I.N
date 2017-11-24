using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // Current Health value 
    protected float currentHealth;

    // fire effect for this monster (to define in the editor)
    public GameObject fireEffect;

    // manager to apply the different effect
    protected Effect currentEffect;

    // Max Health value
    public float maxHealth;

    // bool to say if the entity is dead or not
    protected bool isDead = false;

    // Audio clips
    public AudioClip hurtClip;
    public AudioClip deathClip;

    // The audio of the monster
    protected AudioSource hurtSound;

    public void InitHealth()
    {
        // we set the currentHealth to the maxhealth possible
        currentHealth = maxHealth;
    }

    // return the current Health
    public float GetCurrentHealth() { return this.currentHealth; }

    // return the max Health
    public float GetMaxHealth() { return this.maxHealth; }

    // return true if the entity is dead, otherwise return false
    public bool IsDead(){ return isDead; }

    // remove the amount of damage from the life of the entity
    // also check if it doesn't goes under 0
    public void Damaged(int damage)
    {
        // if the entity is already dead then we just return
        if (isDead)
            return;

        // we remove the amount of damage, and we be carefull it doesn't go under 0
        currentHealth = (this.currentHealth - damage <= 0) ? 0 : this.currentHealth - damage;

        // we finally check if the life is equal to 0
        if (currentHealth == 0)
        {
            isDead = true;
            Death();
        } else
        {
            HurtAnim();
            hurtSound.clip = hurtClip;
            hurtSound.Play();
        }            
    }

    public void Healed(int amount)
    {
        // we add the amount of healing, and we be carefull it doesn't go over the max
        currentHealth = (this.currentHealth + amount >= maxHealth) ? maxHealth : this.currentHealth + amount;
        // update the player life
        HealingAnim();
    } 

    // function must be implemented by child
    public virtual void Death() {
        Debug.Log("No Death Function set!");
    }
    public virtual void HurtAnim() {
        Debug.Log("No Hurting Function set!");
    }
    public virtual void HealingAnim()
    {
        Debug.Log("No Healing Function set!");
    }

}
