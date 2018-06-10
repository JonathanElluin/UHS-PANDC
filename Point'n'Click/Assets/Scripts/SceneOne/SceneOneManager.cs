using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOneManager : MonoBehaviour
{

    private SpriteRenderer VildracSpriteRenderer;
    private AudioSource BackgroundMusicAudioSource;
    public GameObject Background;
    public GameObject ChoixText;
    private Animator CouloirAnimator;
    private float Alpha;

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
        BackgroundMusicAudioSource = GetComponent<AudioSource>();
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracAnimator = Vildrac.GetComponent<Animator>();
        CouloirAnimator = Background.GetComponent<Animator>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        CouloirAnimator.SetBool("isOpen", true);
        VildracAnimator.SetBool("isColere", false);
        VildracAnimator.SetBool("isAlcolo", false);
        BackgroundMusicAudioSource.Play();
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
                Invoke("SetChoiceActive", 0.5f);
            }
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
                    Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
                    if (Alpha <= 0)
                    {

                        Player_controller.startMovingLeft = false;
                        Player_controller.startMovingRight = false;
                        //Debug.Log("alcoolique?" + isAlcolo);
                        int choix = isAlcolo ? 0 : 1;
                        //Debug.Log(choix);
                        PlayerPrefs.SetInt("ChoiceScene1",choix);
                        PlayerPrefs.SetInt("SceneToLoad", 2);
                        Destroy(Vildrac);
                        Application.Quit();
                        //SceneManager.LoadScene(1);
                    }
                }
            }
        }
    }

    private void SetChoiceActive()
    {
        Cursor.SetActive(true);
        VildracColere.SetActive(true);
        ChoixText.SetActive(true);
        VildracAlcolo.SetActive(true);
        Vildrac.SetActive(false);
        isStart = false;
    }

    public void SetChoice()
    {
        switch (SharedObjects.GetInt(CHOICEKEY))
        {
            case 1:
                isAlcolo = true;
                break;
            case 2:
                isColere = true;
                break;
            default:
                break;
        }

        UnsetChoiceActive();
    }

    private void UnsetChoiceActive()
    {
        Destroy(Cursor.gameObject);
        Destroy(VildracAlcolo.gameObject);
        Destroy(VildracColere.gameObject);
        Destroy(ChoixText);
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
        Alpha = 1;
    }
}
