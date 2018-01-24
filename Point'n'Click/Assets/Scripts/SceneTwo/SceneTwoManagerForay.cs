using Assets.Scripts;
using UnityEngine;

public class SceneTwoManagerForay : MonoBehaviour
{

    private SpriteRenderer VildracSpriteRenderer;
    public GameObject Background;

    public GameObject Vildrac;
    private Animator VildracAnimator;

    public GameObject DummyStopPosition;
    public GameObject DummyEndPosition;

    public GameObject Couloir;
    public GameObject Cursor;
    public GameObject VildracAlcolo;
    public GameObject VildracColere;

    private bool isStart = true;

    public static string CHOICEKEY = "Choix";
    private static SceneTwoManagerForay instance;

    public static SceneTwoManagerForay getInstance()
    {
        if (instance == null)
        {
            instance = (SceneTwoManagerForay)FindObjectOfType(typeof(SceneTwoManagerForay));
            return instance;
        }
        return instance;
    }


    // Use this for initialization
    void Start()
    {
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracAnimator = Vildrac.GetComponent<Animator>();
    }

    public void Update()
    {
        VildracManager();
    }

    private void VildracManager()
    {
        PlayerControllerScene2.startMovingLeft = true;
        if (Vildrac.transform.position.x <= DummyStopPosition.transform.position.x)
        {
            PlayerControllerScene2.startMovingLeft = false;
            PlayerControllerScene2.isIddle = true;
        }
        Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
    }

    private void SetChoiceActive()
    {
        Cursor.SetActive(true);
        VildracColere.SetActive(true);
        VildracAlcolo.SetActive(true);
        Vildrac.SetActive(false);
        isStart = false;
    }

    private void UnsetChoiceActive()
    {
        Destroy(Cursor.gameObject);
        Destroy(VildracAlcolo.gameObject);
        Destroy(VildracColere.gameObject);
        Vildrac.SetActive(true);
        VildracAnimator.SetInteger("Speed", 0);
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
    }
}