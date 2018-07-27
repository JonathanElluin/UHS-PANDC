using UnityEngine;
using Assets.Scripts;
using SimpleFirebaseUnity;
using System;
using System.Collections.Generic;
using SimpleFirebaseUnity.MiniJSON;

public class SpriteClickScript : MonoBehaviour {

    public GameObject sprite;
    public GameObject collision;
    private bool curseurPresent = false;

    static int debug_idx = 0;

    private Firebase firebase;
    Firebase lastUpdate;
    Firebase value;
    private String currentValue;
    private string valeur = "";

    [SerializeField]
    TextMesh textMesh;

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
            DebugLog(valeur);
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


    private void Update()
    {
        if ( valeur.Equals("action") && curseurPresent == true)  //Appui sur Espace
        {
            SetCollider();
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curseurPresent = true;
        this.collision = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        curseurPresent = false;
    }

    private void SetCollider()
    {
        switch (sprite.name)
        {
            case "Choix 1":
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 1);
                    break;
                }
            case "Choix 2":
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 2);
                    break;
                }
            default:
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 1);
                    break;
                }
        }
        SceneOneManager.getInstance().SetChoice();
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