using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject wall;
    public GameObject effectAOE;
    public GameObject spawnPoint;
    public GameObject boss;

    protected bool begin;
    protected float timer;

    // For text
    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBoxManager;

    bool textIsActive;

    bool wait;


    // Use this for initialization
    void Start()
    {
        begin = false;
        wait = false;
        theTextBoxManager = FindObjectOfType<TextBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {

        textIsActive = theTextBoxManager.isActive;
        if (begin && !textIsActive)
        {
            timer = Time.time + 5f;
            wait = true;
            begin = false;
        }

        if (wait && timer < Time.time)
        {
            GameObject Instanceboss = (GameObject)Instantiate(boss, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
            Instanceboss.SetActive(true);

            timer = 0;
            wait = false;
        }
    }

    public void Begin()
    {
        wall.SetActive(true);        
        LauchText();
        begin = true;
    }

    public void LaunchAOE()
    {
        effectAOE.SetActive(true);
    }
    public void StopAOE()
    {
        effectAOE.SetActive(false);
    }
    public void DestroyWall()
    {
        wall.SetActive(false);
        Destroy(wall, 2f);
    }

    // Update is called once per frame
    public void LauchText()
    {
        theTextBoxManager.ReloadScript(theText, startLine, endLine);
    }
}
