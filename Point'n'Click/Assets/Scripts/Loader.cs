using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //PlayerPrefs.DeleteAll();
       int scene = PlayerPrefs.GetInt("SceneToLoad",1);
        SceneManager.LoadScene('2');
        
    }
}
