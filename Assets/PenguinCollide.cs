using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PenguinController.Instance.Collide(other.tag);
        //if (other.tag == "WindStart")
        //{
        //    GameManager.Instance.StartWind();
        //    //windFlag = true;
        //    //Debug.Log("Wind Start.");
        //}
        //if (other.tag == "Exit")
        //{
        //    Application.Quit();
        //}
        //if (other.tag == "Bird")
        //{
        //    //Application.Quit();
        //    //Fall();

        //}
    }
}
