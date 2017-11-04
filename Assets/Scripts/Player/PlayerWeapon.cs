using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {


    // timer to next change
    public float timeBetweenWeaponChange = 0.05f;

    // Weapon arrow, and highlight circle
    public GameObject weaponArrow;
    public RectTransform weaponHighlight;

    // general timer to know the time between two update
    float timer;

    int weaponID;


    void Start()
    {
        weaponID = 0;
    }

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && timer > timeBetweenWeaponChange) // next weapon
        {
            ChangeWeapon(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && timer > timeBetweenWeaponChange) // previous weapon
        {
            ChangeWeapon(false);
        }

        if (Input.GetKey("1") && timer > timeBetweenWeaponChange) //  weapon 1
        {
            SelectWeapon(0);
        }
        else if (Input.GetKey("2") && timer > timeBetweenWeaponChange) //  weapon 2
        {
            SelectWeapon(1);
        }
        else if (Input.GetKey("3") && timer > timeBetweenWeaponChange) //  weapon 3
        {
            SelectWeapon(2);
        }
        else if (Input.GetKey("4") && timer > timeBetweenWeaponChange) //  weapon 4
        {
            SelectWeapon(3);
        }
    }


    public void ChangeWeapon(bool next)
    {
        timer = 0f;

        Vector3 rot;
        if (next)
        {
            weaponID++;
            rot = new Vector3(0, 0, -90);
        } else
        {
            weaponID--;
            rot = new Vector3(0, 0, 90);
        }
        if (weaponID == 4)
        {
            weaponID = 0;
            rot = new Vector3(0, 0, 270);
        }
        if (weaponID == -1)
        {
            weaponID = 3;
            rot = new Vector3(0, 0, -270);
        }

        weaponArrow.transform.Rotate(rot);
        UpdateHighlight();

    }

    public void SelectWeapon(int id)
    {
        timer = 0f;

        Vector3 rot;
        if (id < 4 && id >= 0) 
        {
            float rz = (-90) * (id - weaponID);

            weaponID = id;
            rot = new Vector3(0, 0, rz);
            weaponArrow.transform.Rotate(rot);
            UpdateHighlight();
        }
    }

    public void UpdateHighlight()
    {
        switch (weaponID)
        {
            case 1: // weapon 2
                weaponHighlight.anchorMin = new Vector2(1f, 0.5f);
                weaponHighlight.anchorMax = new Vector2(1f, 0.5f);
                weaponHighlight.pivot = new Vector2(1f, 0.5f);
                break;

            case 2: // weapon 3
                weaponHighlight.anchorMin = new Vector2(0.5f, 0f);
                weaponHighlight.anchorMax = new Vector2(0.5f, 0f);
                weaponHighlight.pivot = new Vector2(0.5f, 0f);
                break;

            case 3: // weapon 4
                weaponHighlight.anchorMin = new Vector2(0f, 0.5f);
                weaponHighlight.anchorMax = new Vector2(0f, 0.5f);
                weaponHighlight.pivot = new Vector2(0f, 0.5f);
                break;

            default: // weapon 1
                weaponHighlight.anchorMin = new Vector2(0.5f, 1f);
                weaponHighlight.anchorMax = new Vector2(0.5f, 1f);
                weaponHighlight.pivot = new Vector2(0.5f, 1f);
                break;
        }
    }
}
