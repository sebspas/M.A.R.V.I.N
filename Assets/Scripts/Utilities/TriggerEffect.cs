using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffect : MonoBehaviour {

    public GameObject effectToTrigger;

    bool isActive;
    bool isWaiting;

    float time = 0f;

    private void Awake()
    {
        isActive = false;
        isWaiting = false;
        effectToTrigger.SetActive(isActive);
    }

    private void Update()
    {
        if (isWaiting && !isActive)
        {
            if (time < Time.time)
            {
                effectToTrigger.SetActive(isActive);
                isWaiting = false;
            }
            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            print("isActive = " + isActive);
            isActive = true;
            effectToTrigger.SetActive(isActive);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
            //effectToTrigger.SetActive(isActive);
            isWaiting = true;
            time = Time.time + 5f;
            print("exit" + isActive);
        }
    }
}