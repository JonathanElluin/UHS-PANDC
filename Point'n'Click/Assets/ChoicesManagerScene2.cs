using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesManagerScene2 : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Color spriteColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("NIQUE BIEN TA MERE COLIDER");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
