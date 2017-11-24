using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject controlsUI;

    // general timer to know the time between two update
    float timer;
    GameObject mainMenu;

    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1;
        mainMenu = GameObject.FindGameObjectWithTag("MainMenuUI");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //loads main level
    public void LoadLevel()
    {
        SceneManager.LoadScene("Main");
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
