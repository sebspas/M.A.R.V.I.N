using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    // The object in wich the script is
    GameObject iceGameplay;
    public GameObject wall;
    public GameObject effect;

    bool begin;

    // Use this for initialization
    void Start()
    {
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Begin()
    {
        begin = true;
        print("bonjour conard");
        iceGameplay = GameObject.FindGameObjectWithTag("IceGameplay");
        wall.SetActive(true);
        effect.SetActive(true);
    }
}
