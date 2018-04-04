using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;
    public static TextBoxManager instance;
    public Text theText;

    public TextAsset[] textFilesAlcolo;
    public TextAsset[] textFilesColere;
    public TextAsset[] textFilesChoix1_2;
    public TextAsset[] textFilesChoix3;
    public RoutineScriptScene2 manager;
    
    public bool isChoice1_2;

    private List<TextAsset> textFiles = new List<TextAsset>();
    private int currentTextFile = 0;
    public string[] textLines;

    private int currentLine = 0;
    private int endAtLine = 0;
    private bool isWithNextStep;

    public PlayerController player;

    public bool isActivated;
    public float speedTextDefil;

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
        switch (PlayerPrefs.GetInt("ChoiceScene1", 0))
        {
            case 0:
                foreach (var t in textFilesAlcolo)
                {
                    textFiles.Add(t);
                }
                break;
            case 1:
                foreach (var t in textFilesColere)
                {
                    textFiles.Add(t);
                }
                break;
            default:
                break;
        }
        Setup();
    }



    void Update()
    {
       

        if (currentLine < textLines.Length)
        {
            theText.text = textLines[currentLine];
        }

        if (currentLine > endAtLine)
        {
            currentTextFile++;
            ResetValues();
            Setup();
            DisableTextBox();
        }
    }

    public void AddChoicesText()
    {
        if (isChoice1_2)
        {
            foreach(var t in textFilesChoix1_2)
            {
                textFiles.Add(t);
            }
        }
        else
        {
            foreach (var t in textFilesChoix3)
            {
                textFiles.Add(t);
            }
        }
        Setup();
    }

    private void ResetValues()
    {
        currentLine = 0;
        endAtLine = 0;
    }

    private void Setup()
    {
         if (currentTextFile < textFiles.Count)
        {
            if (textFiles.Count != 0)
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
            DisableTextBox();
        }

    }

    public void EnableTextBox()
    {
        if (textBox != null)
        {
            isActivated = true;
            StartCoroutine("TextCoroutine");
            textBox.SetActive(true);
            isWithNextStep = false;
        }

    }

    public void EnableTextBoxWithNextStep()
    {
        if (textBox != null)
        {
            isActivated = true;
            StartCoroutine("TextCoroutine");
            textBox.SetActive(true);
            isWithNextStep = true;
        }
    }

    public void DisableTextBox()
    {
        StopCoroutine("TextCoroutine");
        isActivated = false;
        textBox.SetActive(false);
        if (isWithNextStep)
        {
            manager.NextStep();
        }         
    }

    public static TextBoxManager getInstance()
    {
        return instance;
    }
    
    public IEnumerator TextCoroutine()
    {
        if(isActivated)
        {
            yield return new WaitForSeconds(speedTextDefil);
            currentLine++;
            StartCoroutine("TextCoroutine");
        }
    }

}
