using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI UIfinalScore;
    [SerializeField]
    TextMeshProUGUI UIbestScore;

    private void Awake()
    {
        if (GameManager.Instance.playTime != 0)
        {
            int finalScoreMinutes = (int)GameManager.Instance.playTime / 60;
            int finalScoreSeconds = (int)GameManager.Instance.playTime % 60;
            int bestScoreMinutes = (int)GameManager.Instance.bestPlayTime / 60;
            int bestScoreSeconds = (int)GameManager.Instance.bestPlayTime % 60;
            UIfinalScore.text = finalScoreMinutes.ToString() + " : " + finalScoreSeconds.ToString();
            UIbestScore.text = bestScoreMinutes.ToString() + " : " + bestScoreSeconds.ToString();
        }
        
    }
    public void OnClickLoadMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
