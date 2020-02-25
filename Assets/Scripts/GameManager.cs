using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WalkStatus
{
    left = 1,
    right = 2
};

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    private float windSpeed = 0f;
    private bool windFlag = false;
    private float timer = 0f;
    public Text windText;
    public Text tipsText;
    private int dir = 0;
    private bool flag = false;
    public GameObject DiePanel;
    private float birdTimer = 0;

    public GameObject BirdPrefab;
    private void Awake()
    {
        DiePanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void Update()
    {
        if (GameManager.fell)
        {
            flag = true;
            if (GameManager.fellForward)
            {
                tipsText.text = "Press <color=red>Triangle</color> to stand up.";
            }
            else
            {
                tipsText.text = "Press <color=blue>X</color> to stand up.";
            }
        }
        else
        {
            if (flag)
            {
                tipsText.text = "Hold <color=red>R1</color> or <color=blue>L1</color> to keep balance.";
            }
            else
            {
                tipsText.text = "Press <color=red>L2</color> and <color=blue>R2</color> alternately to walk.";
            }
        }
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
                windSpeed = Random.Range(10.0f, 15.0f);
                timer = 0;
            }
            
        }
        windText.text = "Wind Speed: " + windSpeed + "\nDirection: " + dir;

        // Bird
        if(birdTimer <= 20)
        {
            birdTimer += Time.deltaTime;
        }
        else
        {
            birdTimer = 0;
            Vector3 pp = PenguinController.Instance.transform.position;
            GameObject.Instantiate(BirdPrefab, new Vector3(pp.x, pp.y + 8.1f, pp.z - 55f), new Quaternion());
        }

        /// Restart
        /// 
        if (Die)
        {
            if (Input.GetButtonDown("Options"))
            {
                Die = false;
                SceneManager.LoadScene(3);
            }
        }
    }

    public float GetWindSpeed()
    {
        if(dir == 0)
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
        //DieText.color = new Color(255, 0, 0, 255);
        DiePanel.GetComponent<CanvasGroup>().alpha = 1;
    }
}
