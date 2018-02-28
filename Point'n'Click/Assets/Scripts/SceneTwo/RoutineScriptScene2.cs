using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineScriptScene2 : MonoBehaviour
{

    public GameObject Vildrac;

    public Transform DummyPositions;
    private List<Transform> DummyStopsPositionArray = new List<Transform>();

    private int currentPosition = 0;
    public TextBoxManager myDialogManager;
    private Animator VildracAnimator;
    private SpriteRenderer VildracSpriteRenderer;
    public Transform WomanSpawn;
    public GameObject Woman;
    public GameObject Background;
    public GameObject Cursor;

    public Sprite CursorAlcoolique;
    public Sprite CursorColerique;

    private SpriteRenderer CursorRenderer;

    private Animator BureauAnimator;
    private float Alpha;
    private float AlphaWoman;

    // Use this for initialization

    void Start()
    {
        VildracAnimator = Vildrac.GetComponent<Animator>();
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        BureauAnimator = Background.GetComponent<Animator>();
        CursorRenderer = Cursor.GetComponent<SpriteRenderer>();
        Cursor.SetActive(false);
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        Alpha = 0;
        myDialogManager.DisableTextBox();
        InitPositions();
        InitSprites();
       // PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Alpha >= 1)
        {
            if (currentPosition == 0)
            {
                myDialogManager.EnableTextBox();
                currentPosition++;
            }
            else if (!myDialogManager.isActive && currentPosition == 1)
            {
                BureauAnimator.SetBool("IsOpen", true);
                Woman = Instantiate(Woman, WomanSpawn.position, WomanSpawn.rotation);
                Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
                currentPosition++;
            }else if(currentPosition == 2 && BureauAnimator.GetCurrentAnimatorStateInfo(0).IsName("BureauPorteOuverte"))
            {
                if(AlphaWoman <= 1)
                {
                    AlphaWoman += 0.02f;
                    Woman.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, AlphaWoman);
                }
                else
                {
                    myDialogManager.EnableTextBox();
                    currentPosition++;
                }
            }else if(currentPosition == 3 && !myDialogManager.isActive)
            {
                SetChoiceActive();
            }
        }
        else
        {
            Alpha += 0.05f;
            VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
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
}
