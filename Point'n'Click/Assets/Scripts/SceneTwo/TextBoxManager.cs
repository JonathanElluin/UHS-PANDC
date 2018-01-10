using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;
    public static TextBoxManager instance;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerController player;

    public bool isActive;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

        //       player = FindObjectOfType<PlayerController>;

        if (textfile != null)
        {

            textLines = (textfile.text.Split('\n'));
        }

        if (endAtLine == 0) ;
        {
            endAtLine = textLines.Length - 1;
        }


    }

    

    void Update()
    {
        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return) && isActive)
        {
            currentLine += 1;

        }

        if(currentLine > endAtLine)
        {
            DisableTextBox();

        }
    }

    public void EnableTextBox()
    {
        isActive = true;
        textBox.SetActive(true);

    }

    public void DisableTextBox()
    {
        isActive = false;
        textBox.SetActive(false);
    }

    public static TextBoxManager getInstance()
    {
        return instance;
    }
}
