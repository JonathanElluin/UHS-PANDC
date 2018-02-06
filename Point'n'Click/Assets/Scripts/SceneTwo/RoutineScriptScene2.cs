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
    public GameObject Background;
    private Animator BureauAnimator;
    private float Alpha;

    // Use this for initialization

    void Start()
    {
        VildracAnimator = Vildrac.GetComponent<Animator>();
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        Alpha = 0;
        myDialogManager.DisableTextBox();
        InitPositions();
        InitSprites();
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

                Debug.Log("suite"); 
            }
        }
        else
        {
            Alpha += 0.05f;
            VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        }





        //    if ((VildracAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_vildrac_alcolo") || VildracAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_vildrac_colere")) && currentPosition == 0)
        //    {
        //        Player_controller.startMovingLeft = true;           
        //    }
        //    if (Player_controller.startMovingLeft && currentPosition == 0)
        //    {
        //        if (Alpha <= 1)
        //        {
        //            Alpha += 0.02f;
        //        }
        //        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        //        if (Vildrac.transform.position.x <= DummyStopsPositionArray[currentPosition].position.x)
        //        {
        //            myDialogManager.EnableTextBox();
        //            Player_controller.startMovingLeft = false;
        //            Player_controller.isIddle = true;
        //            currentPosition++;
        //        }
        //    }

        //if (!myDialogManager.isActive && currentPosition == 1)
        //    {
        //        Player_controller.isIddle = false;
        //        Player_controller.startMovingLeft = true;
        //        if (Vildrac.transform.position.x <= DummyStopsPositionArray[currentPosition].position.x)
        //        {
        //            myDialogManager.EnableTextBox();
        //            Player_controller.startMovingLeft = false;
        //            Player_controller.isIddle = true;
        //        }
        //    }
    }

    public void InitPositions()
    {
        foreach (Transform t in DummyPositions)
        {
            DummyStopsPositionArray.Add(t);
        }
    }

    public void InitSprites()
    {
        switch (PlayerPrefs.GetInt("ChoiceScene1", 0))
        {
            case 0:
                VildracAnimator.SetBool("isAlcolo", true);
                VildracAnimator.SetBool("isColere", false);
                break;
            case 1:
                VildracAnimator.SetBool("isAlcolo", false);
                VildracAnimator.SetBool("isColere", true);
                break;
        }
    }
}
