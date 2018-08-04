using UnityEngine;
using System.Collections;
using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;
using System;
using System.Collections.Generic;

public class MoveScript : MonoBehaviour
{
    static int debug_idx = 0;
    string valeur = "";
    private Firebase firebase;
    Firebase lastUpdate;
    Firebase value;
    private String currentValue;

    public Transform MaxLeft;
    public Transform MaxRight;
    public Transform MaxTop;
    public Transform MaxBot;

    [SerializeField]
    TextMesh textMesh;
    public AudioSource myAudioSource;
    public float Speed;

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
        FirebaseObserver observerButton = new FirebaseObserver(value, 0.0001f);
        observerButton.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            valeur = snapshot.Value<string>();
            currentValue = valeur;
        };
        observerButton.Start();
    }

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(Tests());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Tests());
        // un obeserveur sur la date
       // lastUpdate = firebase.Child("bouton/date");
        //un observer sur la touche
        value = firebase.Child("bouton/value");
       
        
        if (currentValue == "droite" && transform.position.x < MaxRight.position.x) //droite
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (currentValue == "gauche" && transform.position.x > MaxLeft.position.x) //gauche
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }

        if (currentValue == "haut" && transform.position.y < MaxTop.position.y)  //haut
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        if (currentValue == "bas" && transform.position.y > MaxBot.position.y)  //bas
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
        }

        if(currentValue == "none")
        {
            transform.position = transform.position;
        }

        //Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow)) //droite
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //gauche
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))  //haut
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))  //bas
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
        }

    }

    IEnumerator Tests()
    {
      
        /*--------------------------------------------------------------*/
        
       // DebugLog("[OBSERVER] sur le timetamps " + lastUpdate.FullKey + "!");
        /*--------------------------------------------------------------*/
        yield return null;
        //observer.Stop();
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
       // Debug.Log(str);
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