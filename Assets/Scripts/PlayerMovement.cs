using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private GameManager gameManager;
    private const int fastModeSpeedMultiplayer = 2;
    private float forwardForce = 8000f;
    private float sidewaysForce = 120f;
    private bool isGameOver = false;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(0, 0, forwardForce * fastModeSpeedMultiplayer * Time.deltaTime);
        }
        else
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (gameObject.transform.position.y < 0.9 && !isGameOver)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle" && !isGameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        this.enabled = false;
        PlayerPrefs.SetInt("Score", gameManager.score);
        StartCoroutine(gameManager.GameOver());
    }
}
