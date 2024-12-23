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

    public float playTime = 0;
    public float bestPlayTime = 0;
    public int playerHP = 3;
    public int bossHP = 10; 
    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        if (sceneNum == 2 || sceneNum == 3 || sceneNum == 4)
            playTime += Time.deltaTime;
        UpdateBestTime();
    }

     private void UpdateBestTime()
    {
        if (GameManager.Instance.playTime > GameManager.Instance.bestPlayTime)
            GameManager.Instance.bestPlayTime = GameManager.Instance.playTime;
    }

    public void UpdatePlayerHealth(bool isFallDown = false)
    {
        if (isFallDown)
            playerHP = 0;
        playerHP -= 1;
        if (playerHP <= 0)
        {
            CalScore();
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public bool UpdateBossHealth()
    {   
        bossHP -= 1;
        if (bossHP <= 0)
        {
            return true;
        }
        
        return false;
    }

    public void CalScore()
    {
        if (playerHP <= 1)
            GameManager.Instance.playTime = 0;
    }
}
