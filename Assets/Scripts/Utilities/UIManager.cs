using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    // general timer to know the time between two update
    float timer;

    public GameObject controlsUI;

    public static bool isPaused;
    public static bool isGameOver;
    GameObject pauseMenu;
    GameObject hud;
    GameObject deathMenu;
    GameObject player;
    PlayerHealth playerHealth;

    // Boolean to add effects to the deathmenu
    bool isMenuShown;
    bool isButtonShown;
    Button restartButton;
    Button quitButton;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        hud = GameObject.FindGameObjectWithTag("HUD");
        deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        Button[] buttons = deathMenu.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == "Restart")
            {
                restartButton = button;
            }
            if (button.name == "MainMenu")
            {
                quitButton = button;
            }
        }
        restartButton.onClick.AddListener(onRestartClick);
        quitButton.onClick.AddListener(ExitGame);
        hidePaused();
        hideDeathMenu();
        timer = 0;
        isPaused = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player dies : show the Game Over menu
        if (playerHealth.GetCurrentHealth() <= 0)
        {
            timer += Time.deltaTime;   
            if (Time.timeScale == 1)
            {
                if(timer > 3)
                {
                    Time.timeScale = 0;
                    showDeathMenu();
                }
            }
        }
        
        //uses the p button to pause and unpause the game
        else if (Input.GetKeyDown("escape"))
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
        if (controlsUI.active)
        {
            hideControls();
        }
        pauseMenu.SetActive(false);
        hud.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hideDeathMenu()
    {
        deathMenu.SetActive(false);
        hud.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void showDeathMenu()
    {
        timer = 0;
        deathMenu.SetActive(true);
        hud.SetActive(false);
        isGameOver = true;
        

    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    
    void onRestartClick()
    {
        LoadLevel("Main");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void showControls()
    {
        controlsUI.SetActive(true);
    }

    public void hideControls()
    {
        controlsUI.SetActive(false);
    }
}
