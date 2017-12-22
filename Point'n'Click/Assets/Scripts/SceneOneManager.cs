using UnityEngine;

public class SceneOneManager : MonoBehaviour {

    private SpriteRenderer VildracSpriteRenderer;
    public GameObject Background;
    private Animator CouloirAnimator;
    private float Alpha;

    public GameObject Vildrac;

    public GameObject DummyStopPosition;

    public GameObject Couloir;

    public GameObject Cursor;
    public GameObject VildracAlcolo;
    public GameObject VildracColere;



    // Use this for initialization
    void Start()
    {
        VildracSpriteRenderer = Vildrac.GetComponent<SpriteRenderer>();
        CouloirAnimator = Background.GetComponent<Animator>();
        VildracSpriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        CouloirAnimator.SetBool("isOpen", true);
    }

    public void Update()
    {
        VildracManager();
    }

    private void VildracManager()
    {
        if (CouloirAnimator.GetCurrentAnimatorStateInfo(0).IsName("PorteOuverte"))
        {
            Player_controller.startMovingLeft = true;
            if (Alpha < 1)
            {
                Alpha += 0.05f;
            }
            if (Vildrac.transform.position.x <= DummyStopPosition.transform.position.x)
            {
                Player_controller.startMovingLeft = false;
                Player_controller.isIddle = true;
                Invoke("SetChoiceActive", 0.5f);
            }
            Vildrac.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, Alpha);
        }
    }

    private void SetChoiceActive()
    {
        Cursor.SetActive(true);
        VildracColere.SetActive(true);
        VildracAlcolo.SetActive(true);
        Vildrac.SetActive(false);
    }

}
