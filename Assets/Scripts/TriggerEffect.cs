using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffect : MonoBehaviour {

    public GameObject effectToTrigger;
    BoxCollider boxCollider;

    bool isActive;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        isActive = false;
        effectToTrigger.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        isActive = !isActive;
        effectToTrigger.SetActive(isActive);
    }

}
