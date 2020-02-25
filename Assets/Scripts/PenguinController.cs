using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    private static PenguinController _instance;
    public static PenguinController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Penguin").GetComponent<PenguinController>();
            }
            return _instance;
        }
    }
    public GameObject penguin;
    public GameObject penguinRotation;

    private int fallStep = 0;

    private float forward = 0f;
    private float backward = 0f;
    //private bool forwardFlag = false;
    //private bool backwardFlag = false;
    private float tempRotate = 0;
    public float offset = 5f;
    //private bool windFlag = false;
    //private float windSpeed = 5f;

    public Animator ani;

    //Lerp
    private Vector3 penguinStart;
    private Vector3 penguinEnd;
    private bool walkComplete = true;
    private float frac = 0;
    private float timer = 0;
    private float timeComplete = 0.1f;

    private Vector3 penguinFallStart;
    private Vector3 penguinFallEnd;
    private bool fallComplete = true;
    private float fallFrac = 0;
    private float fallTimer = 0;
    private float fallTimeComplete = 0.2f;

    private Vector3 penguinUpStart;
    private Vector3 penguinUpEnd;
    private bool upComplete = true;
    private float upFrac = 0;
    private float upTimer = 0;
    private float upTimeComplete = 0.2f;

    public GameObject LeftWing;
    public GameObject RightWing;


    // Start is called before the first frame update
    void Start()
    {
        //animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")
        //ani = GameObject.Find("Penguin").GetComponent<Animator>();
        //ani = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Start)
        {
            WalkCheck();
            CheckForward();
            CheckBackward();
        }
        
        //if (GameManager.fell)
        //{
        //    Application.Quit();
        //}

        if (!walkComplete)
        {
            timer += Time.deltaTime;
            frac = timer / timeComplete;
            if (frac < 0)
                frac = 0;
            if (frac > 1)
                frac = 1;
            penguin.GetComponent<Transform>().position = Vector3.Lerp(penguinStart, penguinEnd, frac);
            if (frac == 1)
            {
                timer = 0;
                walkComplete = true;
            }
        }

        if (!fallComplete)
        {
            //Debug.Log("Fall Lerp");
            fallTimer += Time.deltaTime;
            fallFrac = fallTimer / fallTimeComplete;
            if (fallFrac < 0)
                fallFrac = 0;
            if (fallFrac > 1)
                fallFrac = 1;
            penguinRotation.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(penguinFallStart, penguinFallEnd, fallFrac);
            if (fallFrac == 1)
            {
                fallTimer = 0;
                fallComplete = true;
            }
        }

        if (!upComplete)
        {
            //Debug.Log("Up Lerp");
            upTimer += Time.deltaTime;
            upFrac = upTimer / upTimeComplete;
            if (upFrac < 0)
                upFrac = 0;
            if (upFrac > 1)
                upFrac = 1;
            penguinRotation.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(penguinUpStart, penguinUpEnd, upFrac);
            if (upFrac == 1)
            {
                upTimer = 0;
                upComplete = true;
                GameManager.fell = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //penguinRotation.transform.Rotate(new Vector3(0, 0, -10 * Time.deltaTime));
            Debug.Log("Rotation z: " + penguinRotation.transform.eulerAngles.z);
            //ani.Play("Right");
        }
        //
        if (penguinRotation.transform.localEulerAngles.z > 180 && penguinRotation.transform.localEulerAngles.z <= 270)
        {
            //Debug.Log("Left Fall!");
            penguinRotation.transform.localEulerAngles = new Vector3(0 ,0, 270);
            GameManager.fellForward = false;
            GameManager.fell = true;
            PolarBear.Instance.PlayerFall();
        }
        else if (penguinRotation.transform.localEulerAngles.z < 180 && penguinRotation.transform.localEulerAngles.z >= 90)
        {
            penguinRotation.transform.localEulerAngles = new Vector3(0, 0, 90);
            GameManager.fellForward = true;
            GameManager.fell = true;
            PolarBear.Instance.PlayerFall();
        }
        else
        {
            if (GameManager.Start)
            {
                DoRotate();
            }
            
        }
        //DoRotate();
        tempRotate = 0;
        //// Reset
        ///
        if (GameManager.fell)
        {
            GetUp();
        }
        
    }

    void WalkCheck()
    {
        //Right Foot
        if (Input.GetButtonDown("RightFoot") && !GameManager.fell)
        {
            //Application.Quit();
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Right"))
            //if (GameManager.walk == WalkStatus.right)
            {
                //Application.Quit();
                Fall();
            }
            else
            {
                //Application.Quit();
                //Debug.Log("Press right: " + GameManager.walk);
                //GameManager.walk = WalkStatus.right;
                walkComplete = false;
                penguinStart = penguin.GetComponent<Transform>().position;
                penguinEnd = penguinStart;
                timer = 0;

                if (GameManager.right)
                    penguinEnd.z -= 1.5f;
                else
                    penguinEnd.z += 1.5f;
                
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") || ani.GetCurrentAnimatorStateInfo(0).IsName("Left"))
                {
                    ani.SetTrigger("Right");
                }
                //GameManager.walk = WalkStatus.right;
                //ani.Play("Right");
            }
        }

        //Left Foot
        if (Input.GetButtonDown("LeftFoot") && !GameManager.fell)
        {
            //Application.Quit();
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Left") || ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            //if(GameManager.walk == WalkStatus.left)
            {
                //Application.Quit();
                Fall();
            }
            else
            {
                //Application.Quit();
                //Debug.Log("Press left: " + GameManager.walk);
                //GameManager.walk = WalkStatus.left;
                walkComplete = false;
                penguinStart = penguin.GetComponent<Transform>().position;
                penguinEnd = penguinStart;
                timer = 0;

                if (GameManager.right)
                    penguinEnd.z -= 1.5f;
                else
                    penguinEnd.z += 1.5f;
                
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("Right"))
                {
                    ani.SetTrigger("Left");
                }
                //GameManager.walk = WalkStatus.left;
                //ani.Play("Left");
            }
        }
    }

    public void Fall()
    {
        PolarBear.Instance.PlayerFall();
        //Debug.Log("Fall");
        if(penguinRotation.GetComponent<Transform>().localEulerAngles.z >= 0 && penguinRotation.GetComponent<Transform>().localEulerAngles.z <= 90 && fallComplete)
        {
            penguinFallStart = penguinRotation.GetComponent<Transform>().localEulerAngles;
            penguinFallEnd = new Vector3(penguinFallStart.x, penguinFallStart.y, 90);
            GameManager.fell = true;
            fallComplete = false;
        }
        if (penguinRotation.GetComponent<Transform>().localEulerAngles.z >= 270 && penguinRotation.GetComponent<Transform>().localEulerAngles.z <= 360 && fallComplete)
        {
            penguinFallStart = penguinRotation.GetComponent<Transform>().localEulerAngles;
            penguinFallEnd = new Vector3(penguinFallStart.x, penguinFallStart.y, 270);
            GameManager.fell = true;
            fallComplete = false;
        }
    }

    void GetUp()
    {
        if (!GameManager.Die)
        {
            if (GameManager.fellForward)
            {
                if (Input.GetButtonUp("Mouth"))
                {
                    RightWing.GetComponent<PlayWingAni>().PlayAni();
                    upComplete = false;
                    penguinUpStart = penguinRotation.GetComponent<Transform>().localEulerAngles;
                    penguinUpEnd = new Vector3(0, 0, 30);
                    PolarBear.Instance.PlayerGetUp();                    
                }
            }
            else
            {
                if (Input.GetButtonUp("Bend"))
                {
                    LeftWing.GetComponent<PlayWingAni>().PlayAni();
                    upComplete = false;
                    penguinUpStart = penguinRotation.GetComponent<Transform>().localEulerAngles;
                    penguinUpEnd = new Vector3(0, 0, 330);
                    PolarBear.Instance.PlayerGetUp();                    
                }
            }
        }
        

        //if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.RightArrow) && fallStep == 0)
        //{
        //    fallStep = 1;
        //}

        //if (GameManager.fellForward)
        //{
        //    if (Input.GetKeyDown(KeyCode.S) && fallStep == 1)
        //    {
        //        fallStep = 2;
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.DownArrow) && fallStep == 1)
        //    {
        //        fallStep = 2;
        //    }
        //}

        //if (fallStep == 2)
        //{
        //    GameManager.fell = false;
        //    penguinRotation.transform.localEulerAngles = new Vector3(0, 0, 0);
        //    fallStep = 0;
        //}
    }

    void CheckBackward()
    {
        if (Input.GetButton("Backward"))
        {
            forward -= 80 * Time.deltaTime;
            //forwardFlag = true;
            tempRotate += forward;
        }
        else
        {
            //forward = 0f;
            forward += 100 * Time.deltaTime;
            if(forward > 0)
            {
                forward = 0;
            }
            tempRotate += forward;
            //forwardFlag = false;
        }
    }

    void CheckForward()
    {
        if (Input.GetButton("Forward"))
        {
            backward += 80 * Time.deltaTime;
            tempRotate += backward;
            //backwardFlag = true;
        }
        else
        {
            //backward = 0f;
            backward -= 100 * Time.deltaTime;
            if (backward < 0)
            {
                backward = 0;
            }
            tempRotate += backward;
            //backwardFlag = false;
        }
    }

    void DoRotate()
    {
        //float tempRotate = penguinRotation.transform.localEulerAngles.z + forward + backward;
        if (penguinRotation.transform.localEulerAngles.z > 180)
        {
            tempRotate += (penguinRotation.transform.localEulerAngles.z - 360f) / 1.5f - offset;
        }
        else
        {
            tempRotate += (penguinRotation.transform.localEulerAngles.z) / 1.5f + offset;
        }
        tempRotate += GameManager.Instance.GetWindSpeed();
        penguinRotation.transform.Rotate(new Vector3(0, 0, tempRotate * Time.deltaTime));
    }

    public void Collide(string tag)
    {
        if (tag == "WindStart")
        {
            GameManager.Instance.StartWind();
            //windFlag = true;
            //Debug.Log("Wind Start.");
        }
        if (tag == "Exit")
        {
            Application.Quit();
        }
        if (tag == "Bird")
        {
            //Application.Quit();
            Fall();
        }
        if (tag == "Bear")
        {
            //Application.Quit();
            Fall();
            GameManager.Instance.PlayerDie();
        }
    }


}
