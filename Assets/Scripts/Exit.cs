using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneNumber + 1);
        }
    }
}
