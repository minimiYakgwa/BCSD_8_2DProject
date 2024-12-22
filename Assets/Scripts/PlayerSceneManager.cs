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
        if (playerHP <= 1)
            SceneManager.LoadScene("GameOverScene");
        playerHP -= 1;
    }

}
