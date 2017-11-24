using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject wall;
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

    public bool final = false;

    private GameObject InstanceBoss = null;


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
            InstanceBoss = (GameObject)Instantiate(boss, spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
            InstanceBoss.SetActive(true);

            timer = 0;
            wait = false;
        }

        if (InstanceBoss != null && InstanceBoss.GetComponent<BossHealth>().IsDead())
        {
            // we destroy the wall
            DestroyWall();

            // if we are at the end of the game and we kill the boss
            if (final)
            {                
                // then we make the endgame screen appear
                GameObject.FindGameObjectWithTag("HUDEndGame").GetComponent<Animator>().SetTrigger("EndGame");
                // stop the game after we got the time to see the animation of end appear
                Invoke("GameController.StopGame", 2.5f);
            }
        }
    }

    public void Begin()
    {
        wall.SetActive(true);        
        LauchText();
        begin = true;
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
