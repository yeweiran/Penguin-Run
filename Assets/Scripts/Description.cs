using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Description : MonoBehaviour
{

    public Text[] text;

    void Update()
    {
        if(Input.GetButton("Forward") && Input.GetButton("Backward"))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetButton("Forward"))
        {
            text[0].color = Color.yellow;
        }
        else
        {
            text[0].color = Color.red;
        }

        if (Input.GetButton("Backward"))
        {
            text[1].color = Color.yellow;
        }
        else
        {
            text[1].color = Color.blue;
        }

        if (Input.GetButton("LeftFoot"))
        {
            text[2].color = Color.yellow;
        }
        else
        {
            text[2].color = Color.red;
        }

        if (Input.GetButton("RightFoot"))
        {
            text[3].color = Color.yellow;
        }
        else
        {
            text[3].color = Color.blue;
        }

        if (Input.GetButton("Mouth"))
        {
            text[4].color = Color.yellow;
        }
        else
        {
            text[4].color = Color.red;
        }

        if (Input.GetButton("Bend"))
        {
            text[5].color = Color.yellow;
        }
        else
        {
            text[5].color = Color.blue;
        }
    }
}
