using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject TextBox;

    public Text theText;

    public TextAsset textFile;

    public string[] textlines;

    public int currentLine;
    public int endAtLine;

    public PlayerMovement playerMovement;
    // replace this by stop the game

    public bool isActive;



    // Use this for initialization
    void Start()
    {

        if (textFile != null)
        {
            textlines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textlines.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive)
        {
            return;
        }

        theText.text = textlines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }

        if (currentLine > endAtLine)
        {
            isActive = false;
            playerMovement.AllowToMove();
        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();            
        }
    }

    public void EnableTextBox()
    {
        isActive = true;
        TextBox.SetActive(true);
        // To replace
        //playerMovement.ForbidToMove();
    }

    public void DisableTextBox()
    {
        isActive = false;
        TextBox.SetActive(false);
        // To replace
        //playerMovement.AllowToMove();
    }

    public void ReloadScript(TextAsset text,int currentL, int endL)
    {
        if(text != null)
        {
            textlines = new string[1];
            textlines = (text.text.Split('\n'));
        }
        currentLine = currentL;
        endAtLine = endL;
        EnableTextBox();
    }
}
