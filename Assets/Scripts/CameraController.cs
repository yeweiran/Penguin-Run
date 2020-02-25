using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform obj;

    private Vector3 camPos;

    public Animation ani;

    private void Awake()
    {
        ani.Play();
        Debug.Log("Camera Ani!");
    }

    void Update()
    {
        if (!ani.isPlaying)
        {
            GameManager.Start = true;
        }
    }
}
