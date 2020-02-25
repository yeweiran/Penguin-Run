using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowControl : MonoBehaviour
{
    public ParticleSystem snow;

    private ParticleSystem.MainModule main;

    void Start()
    {
        main = snow.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Wind: " + TutorManager.Instance.GetWindSpeed());
        main.startSpeed = -TutorManager.Instance.GetWindSpeed();
        if(TutorManager.Instance.GetWindSpeed() != 0)
        {
            main.maxParticles = Mathf.Abs((int)(TutorManager.Instance.GetWindSpeed() * 100));
        }
    }
}
