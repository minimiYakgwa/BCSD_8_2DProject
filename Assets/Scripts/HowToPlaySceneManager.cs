using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlaySceneManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
