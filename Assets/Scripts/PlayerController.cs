using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text loseText;

    private Rigidbody2D rb2d;
    private int count;
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Redbox"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    public void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You Win! Game created by Hunter Chang! Press E to Restart";
            Time.timeScale = 0;
        }

        if (count == 12)
        {
            transform.position = new Vector3(100, 0);
        }
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            loseText.text = "You Lose! Press E to Restart";
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
        }
    }
}