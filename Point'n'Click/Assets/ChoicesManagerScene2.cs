using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesManagerScene2 : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color spriteColor;
    public static int Choice;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (gameObject.CompareTag("Choix1"))
        {
            Choice = 1;
        }
        if (gameObject.CompareTag("Choix2"))
        {
            Choice = 2;
        }
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Choice = 0;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }


	
}
