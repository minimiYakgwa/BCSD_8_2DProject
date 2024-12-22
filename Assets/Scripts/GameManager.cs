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
}
