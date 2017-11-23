using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static bool isPaused;
    GameObject pauseMenu;
    GameObject hud;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        hud = GameObject.FindGameObjectWithTag("HUD");
        hidePaused();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown("escape"))
        {
            if (Time.timeScale == 1)
            {
                isPaused = true;
                //Debug.Log("paused");
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                //Debug.Log("resume");
                Time.timeScale = 1;
                hidePaused();
                isPaused = false;
            }
        }
    }


    //Reloads the Level
    public void Reload(string level)
    {
        SceneManager.LoadScene(level);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            isPaused = true;
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
            isPaused = false;
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        hud.SetActive(false);
        pauseMenu.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        pauseMenu.SetActive(false);
        hud.SetActive(true);
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    // leave the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
