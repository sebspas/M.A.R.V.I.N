using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonus : MonoBehaviour {

    // timer to next change
    public float timeBetweenWeaponChange = 0.05f;

    // Time to reload each bonus
    const float timeChargeBonusShield = 9f;
    const float timeChargeBonusAura = 15f;
    const float timeChargeBonusBoost = 11.5f;

    // Energy costs for each bonus (starting max energy level : 20)
    const float energyCostBonus1 = 15f;
    const float energyCostBonus2 = 30f;

    // Highlight square
    public RectTransform bonusHighlight;

    // Recharging bonus square
    public Image chargeBonus1;
    public Image chargeBonus2;
    public Image chargeBonus3;

    // slider for the bullet energy/xp
    //public Image energySlider;

    // Max size for the bonus gauge square : when reached, the bonus is fully reloaded
    public float maxGaugeSize = 50;

    // general timer to know the time between two update
    float timer;
    // Timer to know if a bonus can be used or not
    float timerBonusShield;
    float timerBonusAura;
    float timerBonusBoost;

    // Timer to know when to stop the bonus
    float timerEllapsedTimeShield;
    float timerEllapsedTimeBoost;
    float timerEllapsedTimeAura;

    // Duration of each bonus
    public float timeMaxShield = 4.0f;
    public float timeMaxEnergyBoost = 5.0f;
    public float timeMaxAura = 2.5f;

    // boolean to know if enhanced weapon bonus is in use
    public bool bonusBoostInUse;
    public bool bonusShieldInUse;
    public bool bonusAuraInUse;

    // To interact with the currentEnergy, so only one script updates the energySlider
    PlayerShooting playerShooting;

    // Shield power
    public GameObject shieldPower;

    // Aura power
    public GameObject auraPower;

    // Boost AttackSpeed
    public GameObject boostAttackSpeed;

    int bonusID;

    void Start()
    {
        timer = 0;
        bonusID = 0;
        timerBonusShield = 0;
        timerBonusAura = 0;
        timerBonusBoost = 0;

        playerShooting = GetComponentInChildren<PlayerShooting>();

        // Bonus gauges start empty
        chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        chargeBonus3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);

        chargeBonus1.color = new Color32(255, 255, 0, 100);
        chargeBonus2.color = new Color32(255, 255, 0, 100);
        chargeBonus3.color = new Color32(255, 255, 0, 100);
        
        bonusBoostInUse = false;
        bonusShieldInUse = false;
    }


    // Update is called once per frame
    // just manage the bonus swap
    void Update () {

        float t = Time.deltaTime;
        timer += t;       
        timerBonusAura += t;

        if (bonusShieldInUse)
        {
            timerEllapsedTimeShield += t;

            if (timerEllapsedTimeShield > timeMaxShield)
            {
                // we cut the anim
                shieldPower.SetActive(false);
                // we also cut the invulnerability on the player
                bonusShieldInUse = false;              
            }
        } else
        {
            timerBonusShield += t;
        }

        if (bonusAuraInUse)
        {
            timerEllapsedTimeAura += t;

            if (timerEllapsedTimeAura > timeMaxAura)
            {
                // we cut the anim
                auraPower.SetActive(false);

                // we also cut the invulnerability on the player
                bonusAuraInUse = false;
            }
        }
        else
        {
            timerBonusAura += t;
        }

        if (bonusBoostInUse)
        {            
            timerEllapsedTimeBoost += t;

            if (timerEllapsedTimeBoost > timeMaxEnergyBoost)
            {
                // we cut the anim
                boostAttackSpeed.SetActive(false);

                // we also cut the invulnerability on the player
                bonusBoostInUse = false;
            }
        }  else
        {
            timerBonusBoost += t;
        }

        if (Input.GetKeyDown("a") && timer > timeBetweenWeaponChange) //  Shield bonus
        {
            SwitchBonus(0);
            UseBonus(bonusID);
        }
        else
        if (Input.GetKeyDown("e") && timer > timeBetweenWeaponChange) //  Electric aura bonus
        {
            SwitchBonus(1);
            UseBonus(bonusID);
        }
        else
        if (Input.GetKeyDown("r") && timer > timeBetweenWeaponChange) //  Enhanced fire rate bonus
        {
            SwitchBonus(2);
            UseBonus(bonusID);
        }
        else
        if (Input.GetButtonDown("Fire2")) //  Use selected bonus
        {
            UseBonus(bonusID);
        }

        UpdateBonusGauges();
    }

    public void SwitchBonus(int bonus)
    {
        timer = 0;
        if (bonus < 3 && bonus >= 0) bonusID = bonus;

        switch (bonusID)
        {
            case 0: // bonus 1
                bonusHighlight.anchorMin = new Vector2(0.5f, 1f);
                bonusHighlight.anchorMax = new Vector2(0.5f, 1f);
                bonusHighlight.pivot = new Vector2(0.5f, 1f);
                break;
            case 1: // bonus 2
                bonusHighlight.anchorMin = new Vector2(0.5f, 0.5f);
                bonusHighlight.anchorMax = new Vector2(0.5f, 0.5f);
                bonusHighlight.pivot = new Vector2(0.5f, 0.5f);
                break;

            case 2: // bonus 3
                bonusHighlight.anchorMin = new Vector2(0.5f, 0f);
                bonusHighlight.anchorMax = new Vector2(0.5f, 0f);
                bonusHighlight.pivot = new Vector2(0.5f, 0f);
                break;

            default: // Error
                break;
        }
    }
    
    public void UpdateBonusGauges()
    {
        float heightBonus1;
        float heightBonus2;
        float heightBonus3;

        // Bonus 1 = shield
        if (timerBonusShield >= timeChargeBonusShield) 
        {
            chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus1.color = new Color32(0, 200, 255, 115);
        } else
        {
            if (!bonusShieldInUse) // doesn't reload if the player is using it
            {
                heightBonus1 = maxGaugeSize * timerBonusShield / timeChargeBonusShield;
            }
            else
            {
                heightBonus1 = 0;
            }           
            chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightBonus1);
            chargeBonus1.color = new Color32(255, 255, 0, 100);
        }

        // Bonus 2 = Aura
        if (timerBonusAura >= timeChargeBonusAura)
        {
            chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus2.color = new Color32(0, 200, 255, 115);
        } else
        {
            if (!bonusAuraInUse) // doesn't reload if the player is using it
            {
                heightBonus2 = maxGaugeSize * timerBonusAura / timeChargeBonusAura;
            }
            else
            {
                heightBonus2 = 0;
            }           
            chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightBonus2);
            chargeBonus2.color = new Color32(255, 255, 0, 100);
        }

        // Bonus 3 = Enhanced gun
        
        if (timerBonusBoost >= timeChargeBonusBoost)
        {
            chargeBonus3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus3.color = new Color32(0, 200, 255, 115);
        } 
        else
        {
            if (!bonusBoostInUse) // doesn't reload if the player is using it
            {
                heightBonus3 = maxGaugeSize * timerBonusBoost / timeChargeBonusBoost;
            } else
            {
                heightBonus3 = 0;
            }
            chargeBonus3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightBonus3);
            chargeBonus3.color = new Color32(255, 255, 0, 100);
        }
    }

    public void UseBonus(int selectedBonus)
    {
        switch (selectedBonus)
        {
            case 0: // Shield
                    // Add code or function to generate a shield
                if (timerBonusShield > timeChargeBonusShield)
                {
                    if (playerShooting.currentEnergy > energyCostBonus1)
                    {
                        timerBonusShield = 0f;
                        playerShooting.currentEnergy -= energyCostBonus1;

                        // add bonus effect
                        bonusShieldInUse = true;

                        // apply the animation
                        shieldPower.SetActive(true);

                        // we set to 0 the time of use of the shield
                        timerEllapsedTimeShield = 0;
                    } else
                    {
                        // add error feedback, so the player knows he lacks energy
                    }
                }
                break;

            case 1: // Electric aura
                    // Add code or function to generate an aura
                if (timerBonusAura > timeChargeBonusAura)
                {
                    if (playerShooting.currentEnergy > energyCostBonus2)
                    {
                        timerBonusAura = 0f;
                        playerShooting.currentEnergy -= energyCostBonus2;
                        // add bonus effect
                        bonusAuraInUse = true;

                        // apply the animation
                        auraPower.SetActive(true);

                        // we set to 0 the time of use of the shield
                        timerEllapsedTimeAura = 0;
                    }
                    else
                    {
                        // add error feedback, so the player knows he lacks energy
                    }
                }
                break;

            case 2: // Enhanced weapon
                    // Add code or function to enhance fire rate and fire power

                bonusBoostInUse = false;
                // we cut the anim
                boostAttackSpeed.SetActive(false);

                if (timerBonusBoost > timeChargeBonusBoost)
                {
                    timerEllapsedTimeBoost = 0f;
                    timerBonusBoost = 0f;
                    bonusBoostInUse = true;

                    // apply the effect                    
                    boostAttackSpeed.SetActive(true);
                }
                break;

            default:
                break;
        }
    }
}
