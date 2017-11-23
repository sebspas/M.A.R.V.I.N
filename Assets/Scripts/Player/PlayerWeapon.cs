using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {
    // timer to next change
    public float timeBetweenWeaponChange = 0.05f;

    // Weapon arrow, and highlight circle
    public GameObject weaponArrow;
    public RectTransform weaponHighlight;

    // The 4 weapon images and the 'unknown weapon' image
    public Sprite electricWeapon;
    public Sprite iceWeapon;
    public Sprite fireWeapon;
    public Sprite plantWeapon;
    public Sprite unknown;
    
    public Sprite weaponUnlockAnim;

    Image electric;
    Image ice;
    Image fire;
    Image plant;

    Image[] allWeapon;

    PlayerShooting playerShooting;

    // general timer to know the time between two update
    float timer;

    int weaponID;

    void Start()
    {
        // get the player to change his weapon
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();

        weaponID = 0;

        allWeapon = GetComponentsInChildren<Image>();
        foreach (Image weapon in allWeapon)
        {
            switch (weapon.name)
            {
                case "Electric":
                    electric = weapon;
                    electric.sprite = electricWeapon;
                    break;
                case "Ice":
                    ice = weapon;
                    ice.sprite = unknown;
                    break;
                case "Fire":
                    fire = weapon;
                    fire.sprite = unknown;
                    break;
                case "Plant":
                    plant = weapon;
                    plant.sprite = unknown;
                    break;
                default:
                    break;
            }
        }

        // TODO remove that ONLY FOR TEST
        addWeapon();
        addWeapon();
        addWeapon();
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

        else if (Input.GetKey("1") && timer > timeBetweenWeaponChange) //  weapon 1
        {
            SelectWeapon(0);
        }
        else if (Input.GetKey("2") && timer > timeBetweenWeaponChange && playerShooting.maxWeapon >= 2) //  weapon 2
        {
            SelectWeapon(1);
        }
        else if (Input.GetKey("3") && timer > timeBetweenWeaponChange && playerShooting.maxWeapon >= 3) //  weapon 3
        {
            SelectWeapon(2);
        }
        else if (Input.GetKey("4") && timer > timeBetweenWeaponChange && playerShooting.maxWeapon >= 4) //  weapon 4
        {
            SelectWeapon(3);
        }

        // update the player weapon
        playerShooting.currentWeapon = weaponID;
    }


    public void ChangeWeapon(bool next)
    {
        timer = 0f;

        Quaternion rot;
        if (next)
        {
            weaponID++;
        } else
        {
            weaponID--;
        }
        if (weaponID == playerShooting.maxWeapon)
        {
            weaponID = 0;
        }
        if (weaponID == -1)
        {
            weaponID = playerShooting.maxWeapon-1;
        }

        float rz = (-90) * weaponID;
        rot = Quaternion.Euler(0, 0, rz);
        weaponArrow.transform.rotation = rot;
        UpdateHighlight();
    }

    public void SelectWeapon(int id)
    {
        timer = 0f;

        Quaternion rot;
        if (id < 4 && id >= 0) 
        {
            weaponID = id;
            float rz = (-90) * weaponID;

            rot = Quaternion.Euler(0, 0, rz);
            weaponArrow.transform.rotation = rot;
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

    public void addWeapon()
    {
        playerShooting.maxWeapon++;

        switch(playerShooting.maxWeapon)
        {
            case 2:
                ice.sprite = weaponUnlockAnim;
                for (int i = 0; i <= 15; i++)
                {
                    ice.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                ice.sprite = iceWeapon;
                for (int i = 15; i >= 10; i--)
                {
                    ice.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                break;
            case 3:
                fire.sprite = weaponUnlockAnim;
                for (int i = 0; i <= 15; i++)
                {
                    fire.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                fire.sprite = fireWeapon;
                for (int i = 15; i >= 10; i--)
                {
                    fire.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                break;
            case 4:
                plant.sprite = weaponUnlockAnim;
                for (int i = 0; i <= 15; i++)
                {
                    plant.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                plant.sprite = plantWeapon;
                for (int i = 15; i >= 10; i--)
                {
                    plant.transform.localScale = new Vector3(0.1f * i, 0.1f * i, 0.1f * i);
                    //wait(0.2f);
                }
                break;
            default:
                break;
        }
    }
}
