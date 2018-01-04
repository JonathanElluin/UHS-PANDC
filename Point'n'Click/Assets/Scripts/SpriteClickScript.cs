using UnityEngine;
using Assets.Scripts;

public class SpriteClickScript : MonoBehaviour {

    public GameObject sprite;
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
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 1);
                    break;
                }
            case "Choix 2":
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 2);
                    break;
                }
            case "Choix 3":
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 3);
                    break;
                }
            default:
                {
                    SharedObjects.SetInt(SceneOneManager.CHOICEKEY, 1);
                    break;
                }
        }
        SceneOneManager.getInstance().SetChoice();
        Debug.Log(gameObject.name);
    }
}