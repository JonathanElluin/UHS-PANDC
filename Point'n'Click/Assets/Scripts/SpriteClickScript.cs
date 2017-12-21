using UnityEngine;
using Assets.Scripts;

public class SpriteClickScript : MonoBehaviour
{

    public GameObject sprite;
    public Collider2D collider1;
    public GameObject collision;
    private bool curseurPresent = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && curseurPresent == true)  //Appui sur Espace
        {
            SetCollider();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curseurPresent = true;
        this.collision = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        curseurPresent = false;
    }

    private void SetCollider()
    {
        switch (sprite.name)
        {
            case "Choix 1":
                {
                    SharedObjects.SetInt(gameObject.name, 1);
                    break;
                }
            case "Choix 2":
                {
                    SharedObjects.SetInt(gameObject.name, 2);
                    break;
                }
            case "Choix 3":
                {
                    SharedObjects.SetInt(gameObject.name, 3);
                    break;
                }
            default:
                {
                    SharedObjects.SetInt(gameObject.name, 1);
                    break;
                }
        }
        Debug.Log(gameObject.name);
    }
}