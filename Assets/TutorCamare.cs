using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorCamare : MonoBehaviour
{
    public Transform obj;

    private Vector3 camPos;

    public Animation ani;

    private bool flag = true;

    private void Awake()
    {
        ani.Play();
        Debug.Log("Camera Ani!");
    }

    void Update()
    {
        if (!ani.isPlaying)
        {
            TutorManager.Start = true;
            if (flag)
            {
                TutorManager.Instance.CameraStart();
                flag = false;
            }
            
        }
    }
}
