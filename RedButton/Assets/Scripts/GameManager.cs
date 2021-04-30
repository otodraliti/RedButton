using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Text counterText;
    private int counterNum;
    private int highScore;

    private Text timerText;
    private float timerNum;
    private float maxTimerNum = 10f;

    public Sprite Correct;
    public Sprite InCorrect;
    private GameObject mainUI;
    public GameObject deathUI;


    private GameObject rightButton;
    private GameObject leftButton;

    private int Condiotion;
    private bool isDead;
   
    private void Awake()
    {
        counterText = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        
        rightButton = GameObject.FindGameObjectWithTag("RightButton");
        leftButton = GameObject.FindGameObjectWithTag("LeftButton");

        mainUI = GameObject.FindGameObjectWithTag("MainUI");

        highScore = PlayerPrefs.GetInt("HighScore");
    }
    private void Start()
    {
        timerNum = maxTimerNum;
        SwitchPose();
    }


    private bool isloadComplete;

    private void Update()
    {
        
        if (isloadComplete == false)
        {
            highScore = PlayerPrefs.GetInt("highScore");
            isloadComplete = true;
        }
        else
        {
            if (counterNum > highScore)
            {
                highScore = counterNum;
                PlayerPrefs.SetInt("highScore", highScore);
            }
        }

        Debug.Log(highScore);
        timerNum -= Time.deltaTime;
        counterText.text = counterNum.ToString("");
        timerText.text = timerNum.ToString("0.0");
        if (isDead == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void SwitchPose()
    {
        int randomNum = Random.Range(0, 100);
        int RandomColor = Random.Range(0, 100);
        if (randomNum > 50)
        {
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Correct;
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = InCorrect;
            Condiotion = 1;
        }
        else
        {
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = InCorrect;
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Correct;
            Condiotion = 2;
        }
        if (RandomColor <= 25 && RandomColor >= 0)
        {
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        if (RandomColor <= 50 && RandomColor >= 26)
        {
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }
        if (RandomColor <= 100 && RandomColor >= 51)
        {
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    void death()
    {
        mainUI.gameObject.SetActive(false);
        deathUI.gameObject.SetActive(true);
        if (counterNum > highScore)
        {
            highScore = counterNum;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        deathUI.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = counterNum.ToString("");
        deathUI.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = highScore.ToString("");
        isDead = true;
    }
    public void LeftButton()
    {
        switch (Condiotion)
        {
            case 1:
                {
                    death();


                    return;
                }
            case 2:
                {
                    if (maxTimerNum > 1f)
                    {
                        maxTimerNum -= 0.1f;
                    }
                    timerNum = maxTimerNum;
                    counterNum += 1;
                    SwitchPose();
                    return;
                }
        }
    }
    public void RightButton()
    {
        switch (Condiotion)
        {
            case 1:
                {
                    if (maxTimerNum > 1f)
                    {
                        maxTimerNum -= 0.1f;
                    }
                    timerNum = maxTimerNum;
                    counterNum += 1;
                    SwitchPose();
                    return;
                }
            case 2:
                {
                    death();
                    return;
                }
        }
    }
}
