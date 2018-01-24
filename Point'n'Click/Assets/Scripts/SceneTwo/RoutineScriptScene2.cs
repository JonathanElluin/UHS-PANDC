﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineScriptScene2 : MonoBehaviour {

    public GameObject Vildrac;
    public Transform[] DummyStopsPosition;
    private int currentPosition = 0;
    public TextBoxManager myDialogManager;
    private Animator VildracAnimator;
    private SpriteRenderer VildracSpriteRenderer;
    public GameObject Background;
    private Animator BureauAnimator;
    private float Alpha;

    // Use this for initialization

    private void Awake()
    {
        VildracAnimator = Vildrac.GetComponent<Animator>();
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        myDialogManager.DisableTextBox();
        InitSprites();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (VildracAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_vildrac_alcolo") || VildracAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle_vildrac_colere") && currentPosition == 0)
        {
            Player_controller.startMovingLeft = true;
           
        }
        if (Player_controller.startMovingLeft && currentPosition == 0)
        {
            if (Alpha <= 1)
            {
                Alpha += 0.02f;
            }
            VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
            if (Vildrac.transform.position.x <= DummyStopsPosition[currentPosition].position.x)
            {
                myDialogManager.EnableTextBox();
                Player_controller.startMovingLeft = false;
                Player_controller.isIddle = true;
                currentPosition++;
            }
         
        }

        if (!myDialogManager.isActive && currentPosition == 1)
        {
            Player_controller.isIddle = false;
            Player_controller.startMovingLeft = true;
            if (Vildrac.transform.position.x <= DummyStopsPosition[currentPosition].position.x)
            {
                myDialogManager.EnableTextBox();
                Player_controller.startMovingLeft = false;
                Player_controller.isIddle = true;
            }
        }
    }

    public void InitSprites()
    {
        switch(PlayerPrefs.GetInt("ChoiceScene1",0))
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
