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

    // delay to display a character by character
    public float delayText = 0.008f;

    private string textToType = "";
    private int currPos = 0;

    // used to only pass once in the EnableFunction
    private bool alreadyActivated = false;

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

        // we start the function to type the text
        StartCoroutine("WriteTextWithEffect");

        EnableTextBox();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentLine++ < endAtLine)
            {
                // we reset the pos
                currPos = 0;
                // we empty the text box
                theText.text = "";
                // we set the text to type
                textToType = textlines[currentLine];

            }            
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

    private IEnumerator WriteTextWithEffect()
    {   
        while (true)
        {            
            if (currPos < textToType.Length)
            {
                theText.text += textToType[currPos];
                currPos++;
            }
               
            yield return new WaitForSeconds(delayText);
        }
    }

    public void EnableTextBox()
    {
        isActive = true;
        TextBox.SetActive(true);
      
        if (!alreadyActivated)
        {
            alreadyActivated = true;
            // we reset the pos
            currPos = 0;
            // we empty the text box
            theText.text = "";
            // we set the text to type
            textToType = textlines[currentLine];
        }
       
        // To replace
        //playerMovement.ForbidToMove();
    }

    public void DisableTextBox()
    {
        isActive = false;
        TextBox.SetActive(false);
        // we empty the text box
        theText.text = "";

        alreadyActivated = false;
        // To replace
        //playerMovement.AllowToMove();
    }

    public void ReloadScript(TextAsset text, int currentL, int endL)
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
