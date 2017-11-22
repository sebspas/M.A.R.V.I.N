using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBoss
{

    // the boss link to the Effect
    BossHealth bossHealth;
    BossMovement bossMovement;

    // the player health for the life still Earth
    PlayerHealth playerHealth;

    GameObject effectAnim;

    // general timer to know the time between two update
    float timer;
    // timer to know when an effect is over
    float globalTimer;

    // we don't have the earth because it's doesn't apply over time, but just give back life to the player
    enum EffectType
    {
        burning,
        frozen,
        none
    }

    // type of active effect
    EffectType effectType;

    // can be the speed to remove  (frozen) or the life to remove (burning) during the time
    int effectValue = 1;

    // time effect
    float effectTime = 0.0f;

    // apply the effect every each 1/2 second (to improve later)
    float effectInterval = 0.0f;

    // we just passed the boss as a componant
    public EffectBoss(BossHealth boss, BossMovement bossMovement)
    {
        this.bossHealth = boss;
        this.bossMovement = bossMovement;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        //set the effect type as none per default at creation
        effectType = EffectType.none;
    }

    // call every frame by the bossHealth, to apply burning damage or slow, or remove them if the timer is over
    public void applyEffects()
    {
        // we update the timer
        timer += Time.deltaTime;
        globalTimer += Time.deltaTime;

        if (effectType == EffectType.burning)
        {
            // if the time between two interval is pass
            if (timer > effectInterval)
            {
                timer = 0;
                //TODO apply here the animation
                // you can apply the animation on the boss here
                bossHealth.TakeDamage(effectValue);
            }
        }

        if (effectType != EffectType.none)
        {
            // if the effect is over
            if (globalTimer > effectTime)
            {
                if (effectType == EffectType.frozen)
                {
                    // we add back the speed taken
                    bossMovement.ChangeNavSpeed(effectValue);
                }

                // we destroy the fire or ice anim
                GameObject.Destroy(effectAnim);

                // reset all the variable
                effectValue = 1;
                effectTime = 0;
                effectInterval = 0;
                effectType = EffectType.none;

                // we can also remove the animation
            }
        }
        else
        {
            timer = 0;
            globalTimer = 0;
        }
    }

    // call when the boss with this effect is hurt by a bullet
    public int getHurt(BulletScript bullet)
    {
        int damage = 0;

        switch (bullet.typeOfBullet)
        {
            case BulletScript.BulletType.Normal:
                damage = bullet.damagePerShot;
                break;

            case BulletScript.BulletType.Fire:
                damage = bullet.damagePerShot;
                effectType = EffectType.burning;
                effectValue = bullet.burningDamage;
                effectTime = bullet.burningTotalTime;
                effectInterval = bullet.burningInterval;
                // we add the fire animation to the boss
                effectAnim = GameObject.Instantiate(bossHealth.fireEffect, bossHealth.transform.position, Quaternion.identity) as GameObject;
                effectAnim.transform.parent = bossHealth.gameObject.transform;
                break;

            case BulletScript.BulletType.Ice:
                damage = bullet.damagePerShot;
                effectType = EffectType.frozen;
                effectValue = bullet.frozenSlow;
                effectTime = bullet.frozenTotalTime;
                bossMovement.ChangeNavSpeed(-bullet.frozenSlow);
                break;

            case BulletScript.BulletType.Earth:
                damage = bullet.damagePerShot;
                // we heal the player
                playerHealth.Healed(bullet.lifeSteal);              
                break;

            default:
                break;
        }
        // amount of damage to be taken
        return damage;
    }
}