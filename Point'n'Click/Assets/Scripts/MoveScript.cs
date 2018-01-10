using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{

    public float Speed;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow)) //droite
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //gauche
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))  //haut
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))  //bas
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
        }

    }

}