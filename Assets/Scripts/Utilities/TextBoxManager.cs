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



    // Use this for initialization
    void Start()
    {

        if (textFile != null)
        {
            playerMovement.ForbidToMove();
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

        theText.text = textlines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }

        if (currentLine > endAtLine)
        {
            TextBox.SetActive(false);
            playerMovement.AllowToMove();
        }
    }
}
