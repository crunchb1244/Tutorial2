using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{ 
     private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text livesText;
    public Text winText;

    private int lives = 3;

    public AudioSource musicSource;

    public AudioClip victorySound;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        anim = GetComponent<Animator>();
    }

   void Update()
   {
       if (Input.GetKeyDown(KeyCode.D))
       {
           anim.SetInteger("State", 1);
           transform.eulerAngles = new Vector3(0, 0, 0);
       }
       if (Input.GetKeyUp(KeyCode.D))
       {
           anim.SetInteger("State", 0);
       }
       if (Input.GetKeyDown(KeyCode.A))
       {
            anim.SetInteger("State", 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
       }
       if (Input.GetKeyUp(KeyCode.A))
       {
           anim.SetInteger("State", 0);
       }

   }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }
        else if(collision.collider.tag == "Enemy")
        {
        lives = lives - 1;
        SetLivesText();
        Destroy (collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

    }
    void SetLivesText() 
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "Failure. Try Logan Rich's Game Again";
            Destroy(gameObject);
        }
    }
    void SetCountText()
    {
        score.text = scoreValue.ToString();

         if (scoreValue == 5)
        {
            transform.position = new Vector2(95.59f, 0f);
        }
        else if (scoreValue == 10)
        {
            winText.text = "Winner! Game by Logan Rich";
            
             musicSource.Play();
            musicSource.clip = victorySound;
        }

    }
}