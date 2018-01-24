using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;
    public static TextBoxManager instance;
    public Text theText;

    public TextAsset[] textFiles;
    private int currentTextFile = 0;
    public string[] textLines;

    private int currentLine = 0;
    public int endAtLine;

    public PlayerController player;

    public bool isActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        Setup();
    }



    void Update()
    {
        if (currentLine < textLines.Length)
        {
            theText.text = textLines[currentLine];
        }

        if (Input.GetKeyDown(KeyCode.Return) && isActive)
        {
            currentLine++;
        }

        if (currentLine >= endAtLine)
        {
            currentTextFile++;
            Setup();
            DisableTextBox();
        }
    }

    private void Setup()
    {
        currentLine = 0;
        if (currentTextFile < textFiles.Length)
        {
            if (textFiles.Length != 0)
            {

                textLines = (textFiles[currentTextFile].text.Split('\n'));
            }

            if (endAtLine == 0)
            {
                endAtLine = textLines.Length - 1;
            }
        }
        else
        {
            Destroy(textBox);
            Destroy(gameObject);
        }

    }

    public void EnableTextBox()
    {
        if (textBox != null)
        {
            isActive = true;
            textBox.SetActive(true);
        }

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
