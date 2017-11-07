using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonus : MonoBehaviour {

    // timer to next change
    public float timeBetweenWeaponChange = 0.05f;

    // Time to reload each bonus
    const float timeChargeBonus1 = 4f;
    const float timeChargeBonus2 = 6f;
    const float timeChargeBonus3 = 8f;

    // Energy costs for each bonus (starting max energy level : 20)
    const float energyCostBonus1 = 4f;
    const float energyCostBonus2 = 6f;

    // Highlight square
    public RectTransform bonusHighlight;

    // Recharging bonus square
    public Image chargeBonus1;
    public Image chargeBonus2;
    public Image chargeBonus3;

    // slider for the bullet energy/xp
    public Image energySlider;

    // Max size for the bonus gauge square : when reached, the bonus is fully reloaded
    public float maxGaugeSize = 40;

    // general timer to know the time between two update
    float timer;
    // Timer to know if a bonus can be used or not
    float timerBonus1;
    float timerBonus2;
    float timerBonus3;

    // boolean to know if enhanced weapon bonus is in use
    public bool bonus3InUse;

    // To interact with the currentEnergy, so only one script updates the energySlider
    PlayerShooting playerShooting;

    int bonusID;


    void Start()
    {
        timer = 0;
        bonusID = 0;
        timerBonus1 = 0;
        timerBonus2 = 0;
        timerBonus3 = 0;

        playerShooting = GetComponentInChildren<PlayerShooting>();

        // Bonus gauges start empty
        chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        chargeBonus3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);

        chargeBonus1.color = new Color32(255, 255, 0, 100);
        chargeBonus2.color = new Color32(255, 255, 0, 100);
        chargeBonus3.color = new Color32(255, 255, 0, 100);
        
        bonus3InUse = false;
    }


    // Update is called once per frame
    // just manage the bonus swap
    void Update () {

        float t = Time.deltaTime;
        timer += t;
        timerBonus1 += t;
        timerBonus2 += t;

        if (!bonus3InUse)
        {
            timerBonus3 += t;
        } 

        if (Input.GetKeyDown("a") && timer > timeBetweenWeaponChange) //  Next bonus
        {
            NextBonus();
        }
        if (Input.GetButtonDown("Fire2")) //  Use selected bonus
        {
            UseBonus(bonusID);
        }

        UpdateBonusGauges();
    }

    public void NextBonus()
    {
        timer = 0;
        bonusID++;
        if (bonusID == 3) bonusID = 0;

        switch (bonusID)
        {
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

            default: // bonus 1
                bonusHighlight.anchorMin = new Vector2(0.5f, 1f);
                bonusHighlight.anchorMax = new Vector2(0.5f, 1f);
                bonusHighlight.pivot = new Vector2(0.5f, 1f);
                break;
        }
    }
    
    public void UpdateBonusGauges()
    {
        float heightBonus1;
        float heightBonus2;
        float heightBonus3;

        // Bonus 1 = shield
        if (timerBonus1 >= timeChargeBonus1) 
        {
            chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus1.color = new Color32(0, 200, 255, 115);
        } else
        {
            heightBonus1 = maxGaugeSize * timerBonus1 / timeChargeBonus1;
            chargeBonus1.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightBonus1);
            chargeBonus1.color = new Color32(255, 255, 0, 100);
        }

        // Bonus 2 = Electric impulse
        if (timerBonus2 >= timeChargeBonus2)
        {
            chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus2.color = new Color32(0, 200, 255, 115);
        } else
        {
            heightBonus2 = maxGaugeSize * timerBonus2 / timeChargeBonus2;
            chargeBonus2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightBonus2);
            chargeBonus2.color = new Color32(255, 255, 0, 100);
        }

        // Bonus 3 = Enhanced gun
        
        if (timerBonus3 >= timeChargeBonus3)
        {
            chargeBonus3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxGaugeSize);
            chargeBonus3.color = new Color32(0, 200, 255, 115);
        } 
        else
        {
            if (!bonus3InUse) // doesn't reload if the player is using it
            {
                heightBonus3 = maxGaugeSize * timerBonus3 / timeChargeBonus3;
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
                if (timerBonus1 > timeChargeBonus1)
                {
                    if (playerShooting.currentEnergy > energyCostBonus1)
                    {
                        timerBonus1 = 0f;
                        playerShooting.currentEnergy -= energyCostBonus1;
                        // add bonus effect
                    } else
                    {
                        // add error feedback, so the player knows he lacks energy
                    }
                }
                break;

            case 1: // Electric impulse
                    // Add code or function to generate an electric impulse
                if (timerBonus2 > timeChargeBonus2)
                {
                    if (playerShooting.currentEnergy > energyCostBonus2)
                    {
                        timerBonus2 = 0f;
                        playerShooting.currentEnergy -= energyCostBonus2;
                        // add bonus effect
                    }
                    else
                    {
                        // add error feedback, so the player knows he lacks energy
                    }
                }
                break;

            case 2: // Enhanced weapon
                    // Add code or function to enhance fire rate and fire power

                bonus3InUse = false;
                if (timerBonus3 > timeChargeBonus3)
                {
                    timerBonus3 = 0f;
                    bonus3InUse = true;
                }
                break;

            default:
                break;
        }
    }
}
