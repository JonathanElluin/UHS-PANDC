using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{

    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    public GameObject Self;
    private int i = 0;
    public float Speed;
    public static bool startMoving;
    public static bool isIddle;
    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (startMoving)
        {
            playerAnimator.SetInteger("Speed", 1);
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        else
        {
            if (isIddle)
            {
                playerAnimator.SetInteger("Speed", -1);
            }
        }
    }
}