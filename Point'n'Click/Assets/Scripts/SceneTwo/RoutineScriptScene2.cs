﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineScriptScene2 : MonoBehaviour {

    public GameObject Vildrac;
    private SpriteRenderer VildracSpriteRenderer;
    public GameObject Background;
    private Animator CouloirAnimator;
    private float Alpha;

    // Use this for initialization
    void Start()
    {
       // VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        //VildracSpriteRenderer.flipX = true;
        PlayerControllerScene2.startMovingLeft = true;
       // CouloirAnimator = Background.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
