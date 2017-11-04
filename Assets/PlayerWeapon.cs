using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {


    // timer to next change
    public float timeBetweenWeaponChange = 0.1f;

    // Weapon arrow, and highlight circle
    public GameObject weaponArrow;
    public GameObject weaponHighlight;

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

    }
}
