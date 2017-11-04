using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour {

    // timer to next change
    public float timeBetweenWeaponChange = 0.05f;

    // Highlight square
    public RectTransform bonusHighlight;

    // general timer to know the time between two update
    float timer;
    int bonusID;


    void Start()
    {
        bonusID = 0;
    }


    // Update is called once per frame
    // just manage the bonus swap
    void Update () {

        timer += Time.deltaTime;
        
        if (Input.GetKeyDown("a") && timer > timeBetweenWeaponChange) //  Next bonus
        {
            NextBonus();
        }
    }

    public void NextBonus()
    {
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
}
