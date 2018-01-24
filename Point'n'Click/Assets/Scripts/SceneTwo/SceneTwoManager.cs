using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTwoManager : MonoBehaviour {

    private SpriteRenderer VildracSpriteRenderer;
    public GameObject Background;
    private Animator CouloirAnimator;
    private float Alpha;
    private TextBoxManager textBoxManager;
    public GameObject Vildrac;
    private Animator VildracAnimator;

    public GameObject DummyStopPosition;
    public GameObject DummyEndPosition;

    public GameObject Couloir;
    public GameObject Cursor;
    public GameObject VildracAlcolo;
    public GameObject VildracColere;

    private bool isAlcolo = false;
    private bool isColere = false;

    private bool isStart = true;

    public static string CHOICEKEY = "Choix";
    private static SceneOneManager instance;

    public static SceneOneManager getInstance()
    {
        if (instance == null)
        {
            instance = (SceneOneManager)FindObjectOfType(typeof(SceneOneManager));
            return instance;
        }
        return instance;
    }


    // Use this for initialization
    void Start()
    {
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracAnimator = Vildrac.GetComponent<Animator>();
        CouloirAnimator = Background.GetComponent<Animator>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        textBoxManager = TextBoxManager.getInstance();
        textBoxManager.DisableTextBox();
        CouloirAnimator.SetBool("isOpen", true);
        VildracAnimator.SetBool("isColere", false);
        VildracAnimator.SetBool("isAlcolo", false);
    }

    public void Update()
    {
        VildracManager();
    }

    private void VildracManager()
    {
        if (CouloirAnimator.GetCurrentAnimatorStateInfo(0).IsName("PorteOuverte") && isStart)
        {

            Player_controller.startMovingLeft = true;
            if (Alpha <= 1)
            {
                Alpha += 0.05f;
            }
            if (Vildrac.transform.position.x <= DummyStopPosition.transform.position.x)
            {
                Player_controller.startMovingLeft = false;
                Player_controller.isIddle = true;
                textBoxManager.EnableTextBox();
           //   Invoke("SetChoiceActive", 0.5f);
            }

            //if (textBoxManager.currentLine == textBoxManager.endAtLine)
            //{
            //    Player_controller.startMovingLeft = true;
            //}

            Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        }
        else
        {
            if (!isStart)
            {
                Player_controller.startMovingLeft = true;
                if (Vildrac.transform.position.x <= DummyEndPosition.transform.position.x)
                {
                    Alpha -= 0.05f;

                    if (Alpha <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void SetChoiceActive()
    {
        Cursor.SetActive(true);
        VildracColere.SetActive(true);
        VildracAlcolo.SetActive(true);
        Vildrac.SetActive(false);
        isStart = false;
    }

    public void SetChoice()
    {
        

    }

        private void UnsetChoiceActive()
    {
        Destroy(Cursor.gameObject);
        Destroy(VildracAlcolo.gameObject);
        Destroy(VildracColere.gameObject);
        Vildrac.SetActive(true);
        VildracAnimator.SetInteger("Speed", 0);
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        if (isAlcolo)
        {
            VildracAnimator.SetBool("isAlcolo", true);
        }

        if (isColere)
        {
            VildracAnimator.SetBool("isColere", true);
        }

        Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        Alpha = 0;
        while (Alpha <= 1)
        {
            Alpha += 0.05f;
            Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        }

    }
}