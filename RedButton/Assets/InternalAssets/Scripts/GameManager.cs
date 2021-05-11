using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    #region
    private Text counterText;
    private int counterNum;
    private int highScore;

    private Text timerText;
    private float timerNum;
    private float maxTimerNum = 5f;

    private Sprite correct;
    private Sprite inCorrect;
    private AudioSource correctSound;
    private AudioSource inCorrectSound;
    
    private GameObject mainUI;
    public GameObject _DeathUI;


    private GameObject rightButton;
    private GameObject leftButton;

    private int Condiotion;
    private bool isDead;
    private bool isLoadComplete;
    #endregion

    //Main Functions
    #region
    private void Awake()
    {
        Application.targetFrameRate = 60;
        SetVariables();
    }
    private void Start()
    {
        correct = Resources.Load<Sprite>("trollnest_check-normal");
        inCorrect = Resources.Load<Sprite>("trollnest_cancel-normal");

        timerNum = maxTimerNum;
        SwitchPose();
    }
    private void Update()
    {
        SaveNLoad();
        TextManager();
        if (isDead == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
    #endregion

    //Preparation Functions
    #region
    private void SetVariables()
    {
        counterText = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();

        rightButton = GameObject.FindGameObjectWithTag("RightButton");
        leftButton = GameObject.FindGameObjectWithTag("LeftButton");

        mainUI = GameObject.FindGameObjectWithTag("MainUI");

        highScore = PlayerPrefs.GetInt("HighScore");

        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").GetComponent<AudioSource>();
        inCorrectSound = GameObject.FindGameObjectWithTag("InCorrectSound").GetComponent<AudioSource>();
    }


    private void TextManager()
    {
        counterText.text = counterNum.ToString("");
        timerText.text = timerNum.ToString("0.0");
        
        timerNum -= Time.deltaTime;
        if (timerNum <= 0)
        {
            death();
        }
    }
    private void SaveNLoad()
    {
        if (isLoadComplete == false)
        {

            highScore = PlayerPrefs.GetInt("highScore");
            isLoadComplete = true;
        }
        else
        {
            if (counterNum > highScore)
            {
                highScore = counterNum;
                PlayerPrefs.SetInt("highScore", highScore);
            }
        }
    }
    #endregion

    //Game Functionality
    #region
    private void SwitchPose()
    {
        int randomNum = Random.Range(0, 100);
        int RandomColor = Random.Range(0, 100);
        if (randomNum > 50)
        {
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = correct;
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inCorrect;
            Condiotion = 1;
        }
        else
        {
            rightButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inCorrect;
            leftButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = correct;
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

    private void death()
    {
        mainUI.gameObject.SetActive(false);
        _DeathUI.gameObject.SetActive(true);
        if (counterNum > highScore)
        {
            highScore = counterNum;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        _DeathUI.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = counterNum.ToString("");
        _DeathUI.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = highScore.ToString("");
        isDead = true;
    }

    public void LeftButton()
    {
        switch (Condiotion)
        {
            case 1:
                {
                    inCorrectSound.GetComponent<AudioSource>().Play();
                    death();
                    return;
                }
            case 2:
                {
                    if (maxTimerNum > 0.3f)
                    {
                        maxTimerNum -= 0.1f;
                    }
                    timerNum = maxTimerNum;
                    counterNum += 1;
                    correctSound.GetComponent<AudioSource>().Play();
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
                    if (maxTimerNum > 0.3f)
                    {
                        maxTimerNum -= 0.1f;
                    }
                    timerNum = maxTimerNum;
                    counterNum += 1;
                    correctSound.GetComponent<AudioSource>().Play();
                    SwitchPose();
                    return;
                }
            case 2:
                {
                    inCorrectSound.GetComponent<AudioSource>().Play();
                    death();
                    return;
                }
        }
    }
    #endregion
}
