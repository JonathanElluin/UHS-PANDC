﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color spriteColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("NIQUE BIEN TA MERE COLIDER");
    }
}
