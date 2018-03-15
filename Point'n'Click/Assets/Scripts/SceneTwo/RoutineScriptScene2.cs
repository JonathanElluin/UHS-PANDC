using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RoutineScriptScene2 : MonoBehaviour
{

    public GameObject Vildrac;

    public Transform DummyPositions;
    private List<Transform> DummyStopsPositionArray = new List<Transform>();

    private int currentStep = 0;
    public TextBoxManager myDialogManager;
    private Animator VildracAnimator;
    private SpriteRenderer VildracSpriteRenderer;
    public Transform WomanSpawn;
    public Transform DummyParticlePosition;
    public GameObject TocTocParticlePrefab;
    public GameObject Woman;
    public GameObject Background;
    public GameObject Cursor;
    public Transform DummyWindowPosition;
    public GameObject WindowPrefab;

    public Sprite mapsSprite;
    public Sprite demineurSprite;

    public Sprite CursorAlcoolique;
    public Sprite CursorColerique;

    private SpriteRenderer CursorRenderer;

    private Animator BureauAnimator;
    private GameObject Window;
    private GameObject TocTocParticle;
    private float Alpha;
    private float AlphaWoman;
    private SpriteRenderer windowSpriteRenderer;

    // Use this for initialization

    void Start()
    {
        VildracAnimator = Vildrac.GetComponent<Animator>();
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
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
                CursorRenderer.sprite = CursorAlcoolique;
                VildracAnimator.SetBool("isAlcolo", true);
                VildracAnimator.SetBool("isColere", false);
                break;
            case 1:
                CursorRenderer.sprite = CursorColerique;
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
        WindowPrefab.GetComponent<SpriteRenderer>().sprite = mapsSprite;
        Window = Instantiate(WindowPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
        Window.GetComponent<Renderer>().sortingOrder = 25;
        Destroy(Window, 3f);
        Invoke("NextStep", 0.5f);
    }

    public void Step2()
    {
        myDialogManager.EnableTextBox();
        TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
        Invoke("NextStep", 3f);
    }

    public void Step3()
    {
        myDialogManager.EnableTextBoxWithNextStep();
    }

    public void Step4()
    {
        BureauAnimator.SetBool("IsOpen", true);
        Woman = Instantiate(Woman, WomanSpawn.position, WomanSpawn.rotation);
        Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
        NextStep();
    }

    public void Step5()
    {
        while(AlphaWoman <= 1)
        {
            AlphaWoman += 0.02f;
            Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
        }
       
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
            default:
                Step0();
                break;
        }
    }
}
