using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// For the music, give credit to :
// "Blob Monsters Return" (Music)
// by Eric Matyas
// www.soundimage.org

public class MainMenuManager : MonoBehaviour {

    public GameObject controlsUI;
    public GameObject loadingScreen;

    // general timer to know the time between two update
    float timer;
    GameObject mainMenu;

    // Use this for initialization
    void Start()
    {
        loadingScreen.SetActive(false);
        mainMenu = GameObject.FindGameObjectWithTag("MainMenuUI");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //loads main level
    public void LoadLevel()
    {
        loadingScreen.SetActive(true);
        /*
        Animator animatorScreen = loadingScreen.GetComponent<Animator>();
        animatorScreen.SetTrigger("playAnim");
        */
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
