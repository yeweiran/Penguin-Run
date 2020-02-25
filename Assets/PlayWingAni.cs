using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWingAni : MonoBehaviour
{
    public Animation ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAni()
    {
        Debug.Log("Ani Play!");
        ani.Play();
    }
}
