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
        if (isWaiting)
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
        isActive = true;
        effectToTrigger.SetActive(isActive);
    }
    private void OnTriggerExit(Collider other)
    {
        isActive = false;
        //effectToTrigger.SetActive(isActive);
        isWaiting = true;
        time = Time.time + 5f;
    }
}