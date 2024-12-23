using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        GameManager.Instance.playerHP = 3;
        GameManager.Instance.bossHP = 10;
        GameManager.Instance.playTime = 0;
        SceneManager.LoadScene("PlayScene");
    }
    public void OnClickHowToPlayButton()
    {
        GameManager.Instance.playerHP = 3;
        GameManager.Instance.bossHP = 10;
        GameManager.Instance.playTime = 0;
        SceneManager.LoadScene("HowToPlayScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
