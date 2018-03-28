using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClickSourie : MonoBehaviour
{

    public GameObject Mouse;
    public Transform Spawn;
    private bool isInstantiate;


    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D In)
    {

        if (In.gameObject.tag == "Cursor")
        {
            if (!isInstantiate)
            {
                Instantiate(Mouse, Spawn.position, Spawn.rotation);
                isInstantiate = true;
            }

        }

    }

}
