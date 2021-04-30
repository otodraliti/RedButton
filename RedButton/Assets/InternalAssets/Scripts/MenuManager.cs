using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Text highScore;
    private int HighScoreNum;

    private Text motivation;
    public GameObject start;
    public GameObject tutorial;


    void Awake()
    {
        HighScoreNum = PlayerPrefs.GetInt("highScore");
        highScore = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        motivation = GameObject.FindGameObjectWithTag("Motivation").GetComponent<Text>();
        highScore.text = PlayerPrefs.GetInt("highScore").ToString("");
    }

    // Update is called once per frame
    void Update()
    {
        if (HighScoreNum <= 0)
        {
            highScore.gameObject.SetActive(false);
            motivation.gameObject.SetActive(false);
            highScore.transform.parent.gameObject.SetActive(false);
            tutorial.gameObject.SetActive(true);
            start.gameObject.SetActive(false);
        }
        if(HighScoreNum > 15)
        {
            tutorial.gameObject.SetActive(false);
            start.gameObject.SetActive(true);
        }
        if (HighScoreNum > 0 && HighScoreNum < 30)
        {
            motivation.text = "You may need a bit more practice";

        }
        if (HighScoreNum > 31 && HighScoreNum < 60)
        {
            motivation.text = "You are pretty good";
        }
        if (HighScoreNum > 61 && HighScoreNum < 100)
        {
            motivation.text = "You are AMAZING";
        }
        if (HighScoreNum > 100)
        {
            motivation.text = "You are Godlike";
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }


}
