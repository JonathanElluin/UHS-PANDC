using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoutineScriptScene2 : MonoBehaviour
{

    private GameObject Vildrac;

    public Transform DummyPositions;
    private List<Transform> DummyStopsPositionArray = new List<Transform>();

    private int currentStep = 0;
    public TextBoxManager myDialogManager;
    private Animator VildracAnimator;
    private SpriteRenderer VildracSpriteRenderer;
    public Transform WomanSpawn;
    public Transform DummyParticlePosition;
    public GameObject TocTocParticlePrefab;
    public WomanController Woman;
    public GameObject Background;
    public GameObject Cursor;
    public Transform DummyWindowPosition;
    public Transform DummyWomanStopPosition;
    public GameObject WindowPrefab;
    public GameObject soupconneText;

    public GameObject mapsPrefab;
    public GameObject demineurPrefab;
    public GameObject Choices;
    public Sprite CursorAlcoolique;
    public Sprite CursorColerique;

    private SpriteRenderer CursorRenderer;

    private Animator BureauAnimator;
    private GameObject Window;
    private GameObject TocTocParticle;
    private float Alpha;
    private float AlphaWoman;
    private SpriteRenderer windowSpriteRenderer;
    private bool isColerique;
    private bool isChoice = false;

    public Transform DummyVildracAlcoloSpawn;
    public Transform DummyVildracColereSpawn;
    public GameObject vildracPrefab;
    private int Choice = 0;

    // Use this for initialization

    void Start()
    {
        BureauAnimator = Background.GetComponent<Animator>();
        CursorRenderer = Cursor.GetComponent<SpriteRenderer>();
        Cursor.SetActive(false);
        Alpha = 0;
        //myDialogManager.DisableTextBox();
        InitPositions();
        InitSprites();
        PlayStep(0);
        // PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if(isChoice)
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                if (ChoicesManagerScene2.Choice != 0)
                {
                    Choice = ChoicesManagerScene2.Choice;
                    DeleteChoices();
                }
            }
        }
    }

    public void InitPositions()
    {
        foreach (Transform t in DummyPositions)
        {
            DummyStopsPositionArray.Add(t);
        }
    }

    public void SetChoiceActive()
    {
        Cursor.SetActive(true);
    }

    public void InitSprites()
    {
        windowSpriteRenderer = WindowPrefab.GetComponent<SpriteRenderer>();
        switch (PlayerPrefs.GetInt("ChoiceScene1", 0))
        {
            case 0:
                Vildrac = Instantiate(vildracPrefab, DummyVildracAlcoloSpawn.position, DummyVildracAlcoloSpawn.rotation);
                VildracAnimator = Vildrac.GetComponent<Animator>();
                VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
                CursorRenderer.sprite = CursorAlcoolique;
                VildracAnimator.SetBool("isAlcolo", true);
                VildracAnimator.SetBool("isColere", false);
                VildracAnimator.SetBool("isSleeping", true);
                break;
            case 1:
                Vildrac = Instantiate(vildracPrefab, DummyVildracColereSpawn.position, DummyVildracColereSpawn.rotation);
                VildracAnimator = Vildrac.GetComponent<Animator>();
                VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
                CursorRenderer.sprite = CursorColerique;
                isColerique = true;
                VildracAnimator.SetBool("isAlcolo", false);
                VildracAnimator.SetBool("isColere", true);
                break;
        }
    }

    public void Step0()
    {
        myDialogManager.EnableTextBoxWithNextStep();
    }

    public void Step1()
    {
        if (isColerique)
        {
            Window = Instantiate(demineurPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
            Window.GetComponent<Renderer>().sortingOrder = 25;
            Destroy(Window, 3f);
            Invoke("NextStep", 0.5f);
        }
        else
        {
            TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
            Invoke("NextStep", 0.5f);
        }

    }

    public void Step2()
    {
        if (isColerique)
        {
            myDialogManager.EnableTextBoxSTD(3f);
            TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
            Invoke("NextStep", 6f);
        }
        else
        {
            myDialogManager.EnableTextBox();
            Invoke("NextStep", myDialogManager.speedTextDefil * myDialogManager.textLines.Length);
        }
       
    }

    public void Step3()
    {
       
        if (isColerique)
        {
            NextStep();
        }
        else
        {
            TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
            Invoke("StepAlcolique", 0.5f);
        }
        
    }

    public void StepAlcolique()
    {
        VildracAnimator.SetBool("isSleeping", false);
        myDialogManager.EnableTextBoxWithNextStep();
    }

    public void Step4()
    {
       
        BureauAnimator.SetBool("IsOpen", true);
        Woman = Instantiate(Woman, WomanSpawn.position, WomanSpawn.rotation);
        Woman.stopPoisiton = DummyWomanStopPosition;
        Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
        Invoke("NextStep", 1f);
    }

    public void Step5()
    {
        if (!isColerique)
        {
            VildracAnimator.SetBool("isLookingLeft", true);
        }
        StartCoroutine("WomanAppear");
        Woman.isRight = true;
        Woman.speed = 1;
        Invoke("NextStep", 1f);
    }

    public void Step6()
    {
        BureauAnimator.SetBool("IsClosed", true);
        BureauAnimator.SetBool("IsOpen", false);
        Invoke("NextStep",1f);
    }

    public void Step7()
    {
        myDialogManager.EnableTextBoxWithNextStep();
    }

    public void Step8()
    {
        InitChoices();
    }

    public void InitChoices()
    {
        isChoice = true;
        soupconneText.SetActive(true);
        Cursor.SetActive(true);
        Choices.SetActive(true);
    }

    public void DeleteChoices()
    {
        isChoice = false;
        soupconneText.SetActive(false);
        Cursor.SetActive(false);
        Choices.SetActive(false);
        StartStepAfterChoice();
    }

    public void Step9()
    {
        switch (Choice)
        {
            case 1:
                myDialogManager.EnableTextBox();
                Invoke("NextStep", 5f);

                break;
            case 2:
                myDialogManager.EnableTextBox();
                Invoke("NextStep",5f);
                break;
            default:
                break;
        }
    }

    public void Step10()
    {
        Window = Instantiate(mapsPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
        Window.GetComponent<Renderer>().sortingOrder = 25;
        Invoke("NextStep", 2f);
    }

    public void Step11()
    {
        if(Choice == 1)
        {
            myDialogManager.EnableTextBoxWithNextStep();
        }
        else
        {
            myDialogManager.EnableTextBoxWithNextStep();
        }
    }

    public void Step12()
    {
        myDialogManager.EnableTextBoxWithNextStep();
    }

    public void Step13()
    {
        Window = Instantiate(demineurPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
        Window.GetComponent<Renderer>().sortingOrder = 27;
        myDialogManager.EnableTextBox();
    }

    public void StartStepAfterChoice()
    {
        if(Choice == 1)
        {
            myDialogManager.isChoice1_2 = true;
            myDialogManager.AddChoicesText();
        }
        else
        {
            myDialogManager.isChoice1_2 = false;
            myDialogManager.AddChoicesText();
        }
        NextStep();
    }

    public void NextStep()
    {
        currentStep++;
        PlayStep(currentStep);
    }

    public void PlayStep(int step)
    {
        switch (step)
        {
            case 1:
                Step1();
                break;
            case 2:
                Step2();
                break;
            case 3:
                Step3();
                break;
            case 4:
                Step4();
                break;
            case 5:
                Step5();
                break;
            case 6:
                Step6();
                break;
            case 7:
                Step7();
                break;
            case 8:
                Step8();
                break;
            case 9:
                Step9();
                break;
            case 10:
                Step10();
                break;
            case 11:
                Step11();
                break;
            case 12:
                Step12();
                break;
            case 13:
                Step13();
                break;
            default:
                Step0();
                break;
        }
    }

    IEnumerator WomanAppear()
    {
        while (AlphaWoman <= 1)
        {
            AlphaWoman += 0.2f;
            Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

}
