using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private float cameraSpeed = 5.0f;

    private void LateUpdate()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime,
            dir.y * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);
         // this.transform.position = player.transform.position + dir;
    }
}
