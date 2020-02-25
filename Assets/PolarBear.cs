using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBear : MonoBehaviour
{
    private static PolarBear _instance;
    public static PolarBear Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("PolarBear").GetComponent<PolarBear>();
            }
            return _instance;
        }
    }

    private int fallTime = 0;
    private float timer = 0;
    private bool showFlag = false;
    private float dis = 0;
    private bool fallFlag = false;
    private bool stopFlag = false;
    private float stopTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFlag)
        {
            if (fallTime == 1 && showFlag)
            {
                Vector3 pp = transform.TransformPoint(PenguinController.Instance.transform.position);
                Vector3 npp = transform.InverseTransformPoint(pp);
                transform.position = new Vector3(transform.position.x, transform.position.y, npp.z + 28.5f - dis);
                if (dis < 6f)
                {
                    dis += 3f * Time.deltaTime;
                }
                else
                {
                    dis = 0;
                    showFlag = false;
                }

            }
            if (fallTime == 1 && timer < 10)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f * Time.deltaTime);
                timer += Time.deltaTime;
            }
            else if (timer >= 10)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f * Time.deltaTime);
                timer += Time.deltaTime;
                if (timer > 20)
                {
                    timer = 0;
                    fallTime = 0;
                }

                ///back lerp
                ///
                //Vector3 pp = PenguinController.Instance.transform.position;
                //transform.position = new Vector3(pp.x, pp.y, pp.z + 24f);

            }
        }
        else
        {
            stopTimer += Time.deltaTime;
            if(stopTimer > 5)
            {
                stopTimer = 0;
                stopFlag = false;
                showFlag = false;
            }
        }
        
    }

    public void PlayerFall()
    {
        //Debug.Log("Player Fall!");
        if (!stopFlag)
        {
            if (fallTime == 0 && !fallFlag)
            {
                ///Show Head Lerp
                ///
                fallFlag = true;
                //Debug.Log("in Player Fall if()!");
                showFlag = true;
                fallTime++;
                Vector3 pp = transform.TransformPoint(PenguinController.Instance.transform.position);
                Vector3 npp = transform.InverseTransformPoint(pp);
                transform.position = new Vector3(transform.position.x, transform.position.y, npp.z + 28.5f);
            }
            if (fallTime == 1)
            {
                timer = 0;
            }
        }
        
    }

    public void PlayerGetUp()
    {
        fallFlag = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Penguin")
        {
            //Debug.Log("Penguin!!!");
            timer = 0;
            fallTime = 0;
            fallFlag = true;
        }
        if (collision.tag == "Bird")
        {
            //Debug.Log("Bird!!!");
            //timer = 10 + Time.deltaTime;
            stopFlag = true;
        }
    }
}
