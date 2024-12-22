using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUi;
    [SerializeField]
    private TextMeshProUGUI textTime;
    [SerializeField]
    private Image playerHPBar;

    public float currentTime = 0f;
    private int playerHP = 3;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseUi.SetActive(true);
            Time.timeScale = 0f;
        }

        if (!pauseUi.activeSelf)
            Time.timeScale = 1f;

        UpdateUiTime();
        UpdatePlayerHealthBar();
    }
    public void OnClickContinueButton()
    {
        pauseUi.SetActive(false);
    }
    private void UpdatePlayerHealthBar()
    {
        playerHPBar.fillAmount = 1f / 3 * playerHP;
    }

    private void UpdateUiTime()
    {
        //currentTime += Time.deltaTime;
        //int minute = (int)currentTime / 60;
        //int second = (int)currentTime % 60;
        int minute = (int)GameManager.Instance.playTime / 60;
        int second = (int)GameManager.Instance.playTime % 60;
        textTime.text = minute.ToString() + " : " + second.ToString();
    }

    public void UpdatePlayerHealth()
    {
        if (playerHP <= 1)
        {
            CalScore();
            SceneManager.LoadScene("GameOverScene");
        }
        playerHP -= 1;
    }

    public void CalScore()
    {
        if (playerHP <= 1)
            GameManager.Instance.playTime = 0;
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }

}


