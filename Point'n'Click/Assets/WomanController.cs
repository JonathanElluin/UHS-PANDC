using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : MonoBehaviour {

    private Animator womanAnimator;
    public int speed;
    public bool isRight;
    public Transform stopPoisiton;

	// Use this for initialization
	void Start () {
        womanAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isRight)
        {
            womanAnimator.SetBool("IsRight", true);
        }

        if (speed > 0 && transform.position.x <= stopPoisiton.position.x)
        {
            womanAnimator.SetInteger("Speed", 1);
            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        }
        else
        {
            womanAnimator.SetInteger("Speed", 0);
        }
	}
}
