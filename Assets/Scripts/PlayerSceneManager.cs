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
    [SerializeField]
    private Image bossHPBar;
    [SerializeField]
    private Boss boss;

    public float currentTime = 0f;
    private int playerHP = 3;
    private int BossHP = 10;

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
        UpdateBossHealthBar();
    }
    public void OnClickContinueButton()
    {
        pauseUi.SetActive(false);
    }
    private void UpdatePlayerHealthBar()
    {
        playerHPBar.fillAmount = 1f / 3 * GameManager.Instance.playerHP;
    }
    private void UpdateBossHealthBar()
    {
        bossHPBar.fillAmount = 1f / 10 * GameManager.Instance.bossHP;
    }

    private void UpdateUiTime()
    {
        int minute = (int)GameManager.Instance.playTime / 60;
        int second = (int)GameManager.Instance.playTime % 60;
        textTime.text = minute.ToString() + " : " + second.ToString();
    }

    public void OnClickStartButton()
    {
        GameManager.Instance.playerHP = 3;
        GameManager.Instance.bossHP = 10;
        GameManager.Instance.playTime = 0;
        SceneManager.LoadScene("PlayScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }



}


