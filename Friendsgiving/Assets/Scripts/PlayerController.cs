using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 movement;
    public int health;
    public float runSpeed;
    public Vector2 level2;
    public Vector2 level3;
    public Text lifeText;
    public GameObject nextLevel;
    private int levelCounter = 0;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetHealthText();
    }

    void Update()
    {
        //gets directional input from keyboard
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
      


        //checks health amount
        if (levelCounter == 1 && health == 3)
        {
            NextLevelScreen();
            transform.position = level2;
            health = 1;
        }
        else if (levelCounter == 2 && health == 4)
        {
            NextLevelScreen();
            transform.position = level3;
            health = 1;
        }
        else if (levelCounter == 3 && health == 5)
        {

        }

        //game over
        if (health < 1)
        {
            StartOver();
        }

        SetHealthText();
    }

  

    private void FixedUpdate()
    {
        //moves character
        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
    }

    //interactions with friends and enemies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Friend"))
        {
            //Debug.Log("hit");
            other.gameObject.SetActive(false);
            health++;
        }
        else
        {
            other.gameObject.SetActive(false);
            health--;
        }
      

    }

    //changes health display
    void SetHealthText()
    {
        lifeText.text = "Health: " + health.ToString();
    }

    void NextLevelScreen()
    {
        nextLevel.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        nextLevel.gameObject.SetActive(false);
    }

    public void StartOver()
    {
        SceneManager.LoadScene("Main");
    }
}
