using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void OnClickHowToPlayButton()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickLoadMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }




    /*private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }*//*


   [SerializeField]
    private GameObject pauseUi;
    [SerializeField]
    private TextMeshProUGUI textTime;
    [SerializeField]
    private Image playerHPBar;

    private float currentTime = 0f;
    private int playerHP = 3;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseUi.SetActive(true);
            Time.timeScale = 0f;
        }

        UpdateUiTime();
        UpdatePlayerHealthBar();
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void OnClickHowToPlayButton()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickLoadMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickContinueButton()
    {
        pauseUi.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdatePlayerHealthBar()
    {
        playerHPBar.fillAmount = 1f / 3 * playerHP;
    }

    private void UpdateUiTime()
    {
        currentTime += Time.deltaTime;
        int minute = (int)currentTime / 60;
        int second = (int)currentTime % 60;
        textTime.text = minute.ToString() + " : " + second.ToString();
    }

    public void UpdatePlayerHealth()
    {
        if (playerHP < 1)
            SceneManager.LoadScene("GameOverScene");
        playerHP -= 1;
    }

*/
}
