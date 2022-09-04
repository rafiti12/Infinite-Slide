using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OnClickButtons : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            scoreText.text = "Top Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            int score = PlayerPrefs.GetInt("Score");
            scoreText.text = "Score: " + score.ToString();
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
                scoreText.text = "New Top " + scoreText.text;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InfiniteSlide");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
