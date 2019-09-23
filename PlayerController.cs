using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text livesText;

    public Text winText;
    public Text loseText;

    private Rigidbody2D rb2d;
    private int count;
    private int totalCount;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        count = 0;

        lives = 3;

        winText.text = "";

        loseText.text = "";

        SetCountText();

        SetLivesText();
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);



        if (count == 12)
        {
            rb2d.position = new Vector2(100f, 0f);
            count = 0;
            SetCountText();
            rb2d.velocity = Vector2.zero;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            totalCount = totalCount + 1;
            SetCountText();
        }

        else if (other.gameObject.CompareTag("BadPickUp"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
            SetLivesText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (totalCount >= 20)
        {
            winText.text = "You are the Winner!!! Game was created by Steven Ulloa!";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            Destroy(this);
            Destroy(rb2d);

            loseText.text = "You Lose!";
        }
    }
}
