using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Text textCountDown;
    public Text choiceText;
    public int reflexionCountDown;
    public int choiceCountDown;

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

    static int debug_idx = 0;

    private Firebase firebase;
    Firebase lastUpdate;
    Firebase value;
    private String currentValue;
    private string valeur = "";

    [SerializeField]
    TextMesh textMesh;

    private bool hasCoroutineCountStarted;
    private bool startMooving = false;

    private void Awake()
    {
        firebase = Firebase.CreateNew("https://uhs-api-v2.firebaseio.com/Lyon/", "hTdDHZdWprOR0MyMuPa1ikI2QG3jUF4yNqdC63M9");
        // Init callbacks
        firebase.OnGetSuccess += GetOKHandler;
        firebase.OnGetFailed += GetFailHandler;

        // un obeserveur sur la date
        lastUpdate = firebase.Child("bouton/date");
        //un observer sur la touche
        value = firebase.Child("bouton/value");
        /*--------------------------------------------------------------*/
        // observer sur "last update" time stamp
        FirebaseObserver observerButton = new FirebaseObserver(value, 0.1f);
        observerButton.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            valeur = snapshot.Value<string>();
        };
        observerButton.Start();
        /*--------------------------------------------------------------*/
        // observer sur "last update" time stamp
        FirebaseObserver observerTime = new FirebaseObserver(lastUpdate, 0.0001f);
        observerTime.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            //OnKeydown();
            currentValue = valeur;
        };
        observerTime.Start();
    }


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
            if(valeur.Equals("action")){
                if (ChoicesManagerScene2.Choice != 0)
                {
                    Choice = ChoicesManagerScene2.Choice;
                    DeleteChoices();
                }
            }
        }

        if (startMooving)
        {
            Player_controller.startMovingLeft = true;
            if(DummyVildracColereSpawn.position.x >= Vildrac.transform.position.x)
            {
                startMooving = false;
                Player_controller.startMovingLeft = false;
                Player_controller.isIddle = true;
                VildracAnimator.SetBool("isLookingLeft", false);
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
        //choix 0 Alcoolique
        //choix 1 colerique
        switch (PlayerPrefs.GetInt("ChoiceScene1", 1))
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
        if (!isColerique)
        {
            RonflementManager.Play();
            myDialogManager.EnableTextBoxSTD(3f);
            Invoke("NextStep", 3f * myDialogManager.textLines.Length);

        }
        else
        {
            BackgroundMusicManager.Play();
            myDialogManager.EnableTextBoxSTD(4f);
            Invoke("NextStep", myDialogManager.textLines.Length * 4f);
        }
       
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
           // RonflementManager.Stop();
            TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
            Invoke("NextStep",1f);
        }

    }

    public void Step2()
    {
        if (isColerique)
        {
            myDialogManager.EnableTextBoxSTD(3f);
            TocTocParticle = Instantiate(TocTocParticlePrefab, DummyParticlePosition.position, DummyParticlePosition.rotation);
            Invoke("NextStep", 3f * myDialogManager.textLines.Length);
        }
        else
        {
           
            myDialogManager.EnableTextBoxSTD(5);
            Invoke("Blurp", 5f);
        }
       
    }

    public void Blurp()
    {
        RonflementManager.Stop();
        BlurpManager.Play();
        Invoke("NextStep", 5);
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
            Invoke("StepAlcolique", 1f);
        }
        
    }

    public void StepAlcolique()
    {
        BackgroundMusicManager.Play();
        VildracAnimator.SetBool("isSleeping", false);
        myDialogManager.EnableTextBoxSTD(5);
        Invoke("NextStep", 3f);
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
        myDialogManager.EnableTextBoxSTD(6);
        Invoke("NextStep", myDialogManager.textLines.Length * 6f);
    }

    public void Step8()
    {
        InitChoices();
    }

    public void InitChoices()
    {
        isChoice = true;
        soupconneText.SetActive(true);
        choiceText.gameObject.SetActive(true);
        textCountDown.gameObject.SetActive(true);

        Choices.SetActive(true);

        StartCoroutine("countDownRoutine", reflexionCountDown);
    }

    public void DeleteChoices()
    {
        isChoice = false;
        soupconneText.SetActive(false);
        choiceText.gameObject.SetActive(false);
        textCountDown.gameObject.SetActive(false);
        Cursor.SetActive(false);
        Choices.SetActive(false);
        MoveToDesk();
        if (Choice == 1)
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

    public void MoveToDesk()
    {
        startMooving = true;   
    }

    public void Step9()
    {
        if(Choice == 1)
        {
            myDialogManager.EnableTextBox();
            Invoke("NextStep", myDialogManager.speedTextDefil * myDialogManager.speedTextDefil);
        }
        else
        {
            myDialogManager.EnableTextBox();
            currentStep++;

            Invoke("NextStep", myDialogManager.textLines.Length * myDialogManager.speedTextDefil);
        }
    }

    public void Step10()
    {
        if (Choice == 1)
        {
            Window = Instantiate(mapsPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
            Window.GetComponent<Renderer>().sortingOrder = 25;
            Invoke("NextStep", myDialogManager.speedTextDefil + 2f);
        }
 
    }

    public void Step11()
    {
        Debug.Log("Step 11 " + Time.timeSinceLevelLoad);
        if (Choice == 1)
        {

            myDialogManager.speedTextDefil = 5;
            myDialogManager.EnableTextBox();

          //  Invoke("NextStep", myDialogManager.textLines.Length * myDialogManager.speedTextDefil);
        }
        else
        {
            Window = Instantiate(mapsPrefab, DummyWindowPosition.position, DummyWindowPosition.rotation);
            Window.GetComponent<Renderer>().sortingOrder = 25;
            myDialogManager.speedTextDefil = 5;
            myDialogManager.EnableTextBox();
         //   Invoke("NextStep", myDialogManager.textLines.Length * myDialogManager.speedTextDefil);
        }

        Invoke("test", myDialogManager.textLines.Length * myDialogManager.speedTextDefil);
    }

    public void test()
    {
        Debug.Log(Time.timeSinceLevelLoad);
    }

    public void NextStep()
    {
        currentStep++;
        PlayStep(currentStep);
    }

    public void PlayStep(int step)
    {
        Debug.Log(step);
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

    public IEnumerator countDownRoutine(int valueCountDown)
    {
        textCountDown.text = valueCountDown.ToString();
        while (valueCountDown > 0)
        {
            yield return new WaitForSeconds(1);
            valueCountDown--;
            textCountDown.text = valueCountDown.ToString();
        }

        if (hasCoroutineCountStarted)
        {
            if(Choice == 0)
            {
                Choice = 1;
            }
            Cursor.SetActive(false);
            DeleteChoices();
        }
        else
        {
            textCountDown.text = "";
            choiceText.text = "";
            yield return new WaitForSeconds(0.5f);
            Cursor.SetActive(true);
            choiceText.text = "Faites votre choix...";
            textCountDown.text = choiceCountDown.ToString();
            hasCoroutineCountStarted = true;
            StartCoroutine("countDownRoutine", choiceCountDown);
        }
    }

    void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] Get from key: <" + sender.FullKey + ">");
        DebugLog("[OK] Raw Json: " + snapshot.RawJson);

        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        if (keys != null)
            foreach (string key in keys)
            {
                DebugLog(key + " = " + dict[key].ToString());
            }
    }

    void GetFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }

    void SetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] Set from key: <" + sender.FullKey + ">");
    }

    void SetFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] Set from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    void UpdateOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] Update from key: <" + sender.FullKey + ">");
    }

    void UpdateFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] Update from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    void DelOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] Del from key: <" + sender.FullKey + ">");
    }

    void DelFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] Del from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    void PushOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] Push from key: <" + sender.FullKey + ">");
    }

    void PushFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] Push from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    void GetRulesOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        DebugLog("[OK] GetRules");
        DebugLog("[OK] Raw Json: " + snapshot.RawJson);
    }

    void GetRulesFailHandler(Firebase sender, FirebaseError err)
    {
        DebugError("[ERR] GetRules,  " + err.Message + " (" + (int)err.Status + ")");
    }

    void GetTimeStamp(Firebase sender, DataSnapshot snapshot)
    {
        long timeStamp = snapshot.Value<long>();
        DateTime dateTime = Firebase.TimeStampToDateTime(timeStamp);

        DebugLog("[OK] Get on timestamp key: <" + sender.FullKey + ">");
        DebugLog("Date: " + timeStamp + " --> " + dateTime.ToString());
    }

    void DebugLog(string str)
    {

        Debug.Log(str);
        if (textMesh != null)
        {
            textMesh.text += (++debug_idx + ". " + str) + "\n";
        }
    }

    void DebugWarning(string str)
    {
        Debug.LogWarning(str);
        if (textMesh != null)
        {
            textMesh.text += (++debug_idx + ". " + str) + "\n";
        }
    }

    void DebugError(string str)
    {
        Debug.LogError(str);
        if (textMesh != null)
        {
            textMesh.text += (++debug_idx + ". " + str) + "\n";
        }
    }

    Dictionary<string, object> GetSampleScoreBoard()
    {
        Dictionary<string, object> scoreBoard = new Dictionary<string, object>();
        Dictionary<string, object> scores = new Dictionary<string, object>();
        Dictionary<string, object> p1 = new Dictionary<string, object>();
        Dictionary<string, object> p2 = new Dictionary<string, object>();
        Dictionary<string, object> p3 = new Dictionary<string, object>();

        p1.Add("name", "simple");
        p1.Add("score", 80);

        p2.Add("name", "firebase");
        p2.Add("score", 100);

        p3.Add("name", "csharp");
        p3.Add("score", 60);

        scores.Add("p1", p1);
        scores.Add("p2", p2);
        scores.Add("p3", p3);

        scoreBoard.Add("scores", scores);

        scoreBoard.Add("layout", Json.Deserialize("{\"x\": 0, \"y\":10}") as Dictionary<string, object>);
        scoreBoard.Add("resizable", true);

        scoreBoard.Add("temporary", "will be deleted later");

        return scoreBoard;
    }

}
