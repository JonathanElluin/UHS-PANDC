using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineScript : MonoBehaviour {


    private SpriteRenderer VildracSpriteRenderer;
    private Animator CouloirAnimator;
    private float Alpha;

    public GameObject Vildrac;

    public GameObject DummyStopPosition;

    public GameObject Couloir;

    public GameObject Cursor;
    

	// Use this for initialization
	void Start () {
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        VildracSpriteRenderer.flipX = true;
        CouloirAnimator = Couloir.GetComponent<Animator>();
        CouloirAnimator.SetBool("isOpen", true);
    }
	
	// Update is called once per frame
	void Update () {
        VildracManager();
     }

    private void VildracManager()
    {
        if (CouloirAnimator.GetCurrentAnimatorStateInfo(0).IsName("PorteOuverte"))
        {
            Player_controller.startMoving = true;
            if (Alpha < 1)
            {
                Alpha += 0.05f;
            }
            if (Vildrac.transform.position.x <= DummyStopPosition.transform.position.x)
            {
                Player_controller.startMoving = false;
                Player_controller.isIddle = true;
                Cursor.SetActive(true);
            }
            Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        }
    }
}
