using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NIQUE BIEN TA MERE");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("NIQUE BIEN TA MERE COLIDER");
    }
}
