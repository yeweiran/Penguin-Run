using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorManager : MonoBehaviour
{
    private static TutorManager _instance;
    public static TutorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<TutorManager>();
            }
            return _instance;
        }
    }
    static public WalkStatus walk = WalkStatus.left;
    static public bool fell = false;
    static public bool fellForward = false;
    static public bool right = true;
    static public bool Die = false;
    static public bool Start = false;
    static public bool BalanceTutor = false;
    static public bool FallTutor = false;
    static public bool WalkingTutor = false;
    static public bool BirdTutor = false;
    static public bool BalanceTextFlag = true;
    static public bool FallTextFlag = true;
    static public bool WalkingTextFlag = true;
    static public bool BirdTextFlag = true;
    private float windSpeed = 0f;
    private bool windFlag = false;
    private float timer = 0f;
    //public Text windText;
    //public Text tipsText;
    private int dir = 0;
    private bool flag = false;
    public Text DieText;
    private float birdTimer = 0;
    private float fallTutorTimer = 0;
    private float windOffset = 0;

    public GameObject BirdPrefab;
    public Text BalanceText;
    //public Text FallText;
    //public Text WalkText;
    //public Text BirdText;
    public GameObject TutorPanel;

    private void Awake()
    {
        TutorPanel.GetComponent<CanvasGroup>().alpha = 0;        
    }

    private void Update()
    {
        //if (GameManager.fell)
        //{
        //    flag = true;
        //    if (GameManager.fellForward)
        //    {
        //        tipsText.text = "Press <color=red>Triangle</color> to stand up.";
        //    }
        //    else
        //    {
        //        tipsText.text = "Press <color=blue>X</color> to stand up.";
        //    }
        //}
        //else
        //{
        //    if (flag)
        //    {
        //        tipsText.text = "Hold <color=red>R1</color> or <color=blue>L1</color> to keep balance.";
        //    }
        //    else
        //    {
        //        tipsText.text = "Press <color=red>L2</color> and <color=blue>R2</color> alternately to walk.";
        //    }
        //}
        if (Input.GetButton("Backward") || Input.GetButton("Forward"))
        {
            flag = false;
        }

        if (windFlag)
        {
            if (timer < 5)
            {
                timer += Time.deltaTime;

            }
            else
            {
                dir = Random.Range(0, 2);
                windSpeed = Random.Range(5.0f, 10.0f);
                timer = 0;
            }

        }
        else
        {
            windSpeed = 0;
        }
        //windText.text = "Wind Speed: " + windSpeed + "\nDirection: " + dir;
        /// Balance Tutor Text
        /// 
        if (BalanceTutor && BalanceTextFlag)
        {
            BalanceText.text = "Hold <color=red>R1</color> or <color=blue>L1</color> to keep balance.";
            TutorPanel.GetComponent<CanvasGroup>().alpha = 1;
            if (Input.GetButtonDown("Options"))
            {
                BalanceTextFlag = false;
                TutorPanel.GetComponent<CanvasGroup>().alpha = 0;
                BalanceText.text = "";
            }
        }
        /// Fall Tutor Text
        /// 
        if (FallTutor && FallTextFlag)
        {
            BalanceText.text = "Press <color=red>Triangle</color> or <color=blue>X</color> to stand up.";
            TutorPanel.GetComponent<CanvasGroup>().alpha = 1;
            if (Input.GetButtonDown("Options"))
            {
                FallTextFlag = false;
                TutorPanel.GetComponent<CanvasGroup>().alpha = 0;
                BalanceText.text = "";
            }
        }
        /// Walk Tutor Check
        /// 
        if (FallTutor)
        {
            if (!TutorManager.fell)
            {
                fallTutorTimer += Time.deltaTime;
                if(fallTutorTimer > 10)
                {
                    WalkingTutor = true;
                    FallTutor = false;
                }
            }
            else
            {
                fallTutorTimer = 0;
            } 
        }
        /// Walk Tutor Text
        /// 
        if(WalkingTutor && WalkingTextFlag)
        {
            BalanceText.text = "Press <color=red>L2</color> and <color=blue>R2</color> alternately to walk.\nIf player inputs one side twice, Penguin will fall.";
            TutorPanel.GetComponent<CanvasGroup>().alpha = 1;
            if (Input.GetButtonDown("Options"))
            {
                WalkingTextFlag = false;
                TutorPanel.GetComponent<CanvasGroup>().alpha = 0;
                BalanceText.text = "";
            }
        }

        /// Bird Tutor
        /// 
        if (BirdTutor && BirdTextFlag)
        {
            BalanceText.text = "Hit by bird will lead Penguin fall, avoid the bird.";
            TutorPanel.GetComponent<CanvasGroup>().alpha = 1;
            if (Input.GetButtonDown("Options"))
            {
                BirdTextFlag = false;
                TutorPanel.GetComponent<CanvasGroup>().alpha = 0;
                BalanceText.text = "";
                Vector3 pp = TutorPenguinController.Instance.transform.position;
                GameObject.Instantiate(BirdPrefab, new Vector3(pp.x, pp.y + 8.1f, pp.z - 30f), new Quaternion());
            }
        }


    }

    public float GetWindSpeed()
    {
        if (BalanceTutor && !BalanceTextFlag)
        {
            windOffset += 0.5f * Time.deltaTime;
            windSpeed = windOffset;
        }
        if (dir == 0)
        {
            return windSpeed;
        }
        else
        {
            return -windSpeed;
        }
    }

    public void StartWind()
    {
        windFlag = true;
    }

    public void PlayerDie()
    {
        Die = true;
        DieText.color = new Color(255, 0, 0, 255);
    }

    public void CameraStart()
    {
        //BalanceText.color = new Color(0, 0, 0, 255);
        
        //BalanceTextFlag = true;
        BalanceTutor = true;
    }

    public void HitBirdTutor()
    {
        BirdTutor = true;        
    }
}
