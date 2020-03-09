using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D rd2d;

    public float speed;

    public Text scoreText;
    public Text livesText;
    public Text winText;
    public Text loseText;

    private int score;
    private int lives;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private bool facingRight;

    public bool livesReset = false;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = score.ToString();

        score = 0;
        lives = 3;

        winText.text = "";
        loseText.text = "";

        SetScoreText();
        SetLivesText();
        resetLives();
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        Flip(hozMovement);

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        animator.SetFloat("Speed", Mathf.Abs(hozMovement));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            score += 1;
            scoreText.text = score.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }

        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livesText.text = lives.ToString();
            SetLivesText();
        }

        if (collision.collider.tag == "Ground")
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
            }
        }
    }
        public void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 8)
        {
            winText.text = "You Win! Game created by Hunter Chang! Press E to Restart";
            Time.timeScale = 0;
        }

        if (score == 4)
        {
            transform.position = new Vector3(70, 0);
        }
    }

    public void SetLivesText()
    {
        {
            livesText.text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                loseText.text = "You Lose! Press E to Restart";
                Time.timeScale = 0;
            }
        }
    }

    public void resetLives()
    {
        if (score == 4)
        {
            lives = 3;
        }
    }

    private void Flip (float hozMovement)
    {
        if (hozMovement < 0 && !facingRight || hozMovement > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void Update()
    {

        if (Input.GetKey("escape"))
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