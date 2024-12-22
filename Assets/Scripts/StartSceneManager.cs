using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
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
}
