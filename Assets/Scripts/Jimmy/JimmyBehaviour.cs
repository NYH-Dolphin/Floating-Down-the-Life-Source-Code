using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using Random = UnityEngine.Random;

public class JimmyBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public ArrayList balloons = new ArrayList();

    // private float speed = 700;                  // Jimmy 做匀速直线运动的速度
    public Animator animator; // Animator 组件

    public bool isCollide; // 判断是否碰撞
    private const int InvalidTimeTick = 300; // 无敌时间
    private int invalidTimeCount; // 无敌时间记数
    private Renderer jimmyRenderer; // Jimmy 的图片

    private static bool stop; // 控制 Jimmy 是否能移动
    public static bool inConversation;


    // 气球的名字和可以使用的气球
    private static readonly string[] BalloonsName =
    {
        "blue_balloon", "cyan_balloon",
        "grassgreen_balloon", "green_balloon", "orange_balloon", "pink_balloon",
        "purple_balloon", "red_balloon", "skyblue_balloon", "yellow_balloon", "gray_balloon"
    };

    // 记录气球和对应的气球的 localPosition
    private static HashMap<string, Vector3> balloonsPositionLeft = new HashMap<string, Vector3>();
    private static HashMap<string, Vector3> balloonsPositionRight = new HashMap<string, Vector3>();

    // 气球池，池子里的气球可以被随意调用
    private HashSet<string> balloonPool = new HashSet<string>();

    private enum floatingStatus
    {
        RIGHT,
        LEFT
    }

    private floatingStatus status = floatingStatus.LEFT;


    private string _balloonName;
    private string _balloonPath;


    // Start is called before the first frame update
    // 初始情况下拿着三个气球
    void Start()
    {
        jimmyRenderer = gameObject.GetComponent<Renderer>();
        // 初始化 balloonName 和 balloonPosition
        InitialBalloonsAttribute();
        // 初始拿着 3 个气球
        AddBalloon(3);
        BalloonBehaviour.setJimmyBehaviour(this);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject balloon in balloons)
        {
            Vector3 pos = balloon.transform.position;
            pos.z = 0;
            balloon.transform.position = pos;
        }

        if (!stop)
        {
            Float();
            ControlSpeed();
        }

        if (isCollide)
        {
            if (balloons.Count > 0)
                Shake();
        }

        CheckTerminate();

        if (transform.localPosition.y < -850)
            PanelManager.Open<OverPanel>();
    }


    private const float DASHSPEED = 800f;
    private const float MINSPEED = 200f;
    private const float MAXSPEED = 400f;

    // 全局控制墙壁的生成速度 —— 相对是 Jimmy 的移动速度
    private void ControlSpeed()
    {
        float changeSpeed = 500 - balloons.Count * 120f;
        // 冲刺加速
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float currentSpeed = WallBehavior.GetSpeed();
            currentSpeed += Time.deltaTime * 100;
            currentSpeed = currentSpeed >= DASHSPEED ? DASHSPEED : currentSpeed;
            WallBehavior.SetSpeed(currentSpeed);
        }
        // 根据气球个数设定
        else
        {
            changeSpeed = changeSpeed >= MAXSPEED ? MAXSPEED : changeSpeed;
            changeSpeed = changeSpeed <= MINSPEED ? MINSPEED : changeSpeed;
            float currentSpeed = WallBehavior.GetSpeed();
            if (currentSpeed < changeSpeed)
            {
                currentSpeed += Time.deltaTime * 40;
                WallBehavior.SetSpeed(currentSpeed);
            }
            else
            {
                currentSpeed -= Time.deltaTime * 40;
                WallBehavior.SetSpeed(currentSpeed);
            }
        }
    }

    // 无敌时间 + 闪烁
    private void Shake()
    {
        int mod = invalidTimeCount % 100;
        if (mod >= 0 && mod <= 50)
        {
            jimmyRenderer.enabled = false;
            for (int i = 0; i < balloons.Count; i++)
            {
                GameObject balloon = (GameObject)balloons[i];
                balloon.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            jimmyRenderer.enabled = true;
            for (int i = 0; i < balloons.Count; i++)
            {
                GameObject balloon = (GameObject)balloons[i];
                balloon.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (++invalidTimeCount == InvalidTimeTick)
        {
            jimmyRenderer.enabled = true;
            isCollide = false;
            invalidTimeCount = 0;
        }
    }

    // Jimmy 与障碍物的碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Jimmy: OnTriggerEnter2D");
        if (collision.gameObject.name.Contains("obstacle") && !isCollide)
        {
            if (collision.gameObject.name.Contains("obstacle_19"))
            {
                return;
            }

            isCollide = true;
            LostBalloon();
        }
    }


    // 停止 Jimmy 的移动和漂浮
    public static void Pause()
    {
        inConversation = true;
        stop = true;
    }

    // Jimmy 继续移动
    public static void Continue()
    {
        inConversation = false;
        stop = false;
    }


    // Jimmy 的移动
    private const float MAXFLOATSPEED = 500f;
    private const float MINFLOATSPEED = 100f;
    private float floatSpeed = 100f;

    private bool notify = true;

    void Float()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        animator.SetBool("floatLeft", false);
        Vector2 p = transform.position;
        // 向左移动
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);

            floatSpeed = floatSpeed <= MINFLOATSPEED ? MINFLOATSPEED : floatSpeed;
            floatSpeed += Time.deltaTime * 700;
            floatSpeed = floatSpeed >= MAXFLOATSPEED ? MAXFLOATSPEED : floatSpeed;
            // 位置移动
            p.x += moveX * floatSpeed * Time.deltaTime;
            // Animation Controller 的参数状态更新
            // animator.SetBool("floatLeft", true);


            if (status != floatingStatus.LEFT)
            {
                Debug.Log("CHANGE");
                status = floatingStatus.LEFT;
                NotifyBalloons(status);
            }
        }
        // 向右移动
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);

            // Debug.Log("RIGHT");
            floatSpeed = floatSpeed <= MINFLOATSPEED ? MINFLOATSPEED : floatSpeed;
            floatSpeed += Time.deltaTime * 700;
            floatSpeed = floatSpeed >= MAXFLOATSPEED ? MAXFLOATSPEED : floatSpeed;
            p.x += moveX * floatSpeed * Time.deltaTime;


            if (status != floatingStatus.RIGHT)
            {
                Debug.Log("CHANGE");
                status = floatingStatus.RIGHT;
                NotifyBalloons(status);
            }
        }
        // 缓慢停止
        else
        {
            floatSpeed -= Time.deltaTime * 700;
            floatSpeed = floatSpeed <= 0 ? 0 : floatSpeed;
            if (status == floatingStatus.LEFT)
            {
                p.x += -0.7f * floatSpeed * Time.deltaTime;
            }
            else
            {
                p.x += 0.7f * floatSpeed * Time.deltaTime;
            }
        }

        // Debug.Log(floatSpeed);
        transform.position = p;
    }

    // 检查游戏是否终止
    private void CheckTerminate()
    {
        if (balloons.Count == 0)
        {
            WallBehavior.End();
            HeightRecord.Pause();
            CharacterBehaviour.real_stop = true;
            transform.GetComponent<Rigidbody2D>().gravityScale = 30;
        }
    }


    public void FallIntoTheGround()
    {
        transform.GetComponent<Rigidbody2D>().gravityScale = 30;
    }

    /// <summary>
    /// Jimmy 与气球交互的代码
    /// ///////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    // 初始化气球的属性
    private void InitialBalloonsAttribute()
    {
        foreach (string balloonName in BalloonsName)
        {
            balloonPool.Add(balloonName);
        }

        balloonsPositionLeft.Add("orange_balloon", new Vector3(0.883999944f, 1.43000007f, 0));
        balloonsPositionRight.Add("orange_balloon", new Vector3(0.670000017f, 1.13999999f, 0));
        
        balloonsPositionLeft.Add("blue_balloon", new Vector3(1.14f, 2f, 0f));
        balloonsPositionRight.Add("blue_balloon", new Vector3(1.13999927f,2,0));

        balloonsPositionLeft.Add("cyan_balloon", new Vector3(1.5f, 1.19000006f, 0f));
        balloonsPositionRight.Add("cyan_balloon", new Vector3(1.40999997f,1.10000002f,0));
        
        balloonsPositionLeft.Add("grassgreen_balloon", new Vector3(0.8f, 1f, 0f));
        balloonsPositionRight.Add("grassgreen_balloon", new Vector3(1.02999997f,0.50999999f,0));
        
        balloonsPositionLeft.Add("gray_balloon", new Vector3(0.910000026f, -0.910000026f, 0f));
        balloonsPositionRight.Add("gray_balloon", new Vector3(1.11000001f,-1.46000004f,0));
        
        balloonsPositionLeft.Add("red_balloon", new Vector3(1.25f, 2.58f, 0f));
        balloonsPositionRight.Add("red_balloon", new Vector3(1.22000003f,2.27999997f,0));

        balloonsPositionLeft.Add("pink_balloon", new Vector3(1.17f, 2.36f, 0f));
        balloonsPositionRight.Add("pink_balloon", new Vector3(0.860000014f,1.88f,0));
        
        balloonsPositionLeft.Add("yellow_balloon", new Vector3(1.17f, 2.97f, 0f));
        balloonsPositionRight.Add("yellow_balloon", new Vector3(1.17f, 2.97f, 0f));
        
        balloonsPositionLeft.Add("purple_balloon", new Vector3(1.29f, 2.96f, 0f));
        balloonsPositionRight.Add("purple_balloon", new Vector3(1.29f, 2.96f, 0f));

        balloonsPositionLeft.Add("green_balloon", new Vector3(1.37f, 2.16000009f, 0f));
        balloonsPositionRight.Add("green_balloon", new Vector3(1.37f, 2.16000009f, 0f));
        
        balloonsPositionLeft.Add("skyblue_balloon", new Vector3(1.24000001f,2.50999999f,0));
        balloonsPositionRight.Add("skyblue_balloon", new Vector3(1.24000001f,2.50999999f,0));
    }


    // 指定 Jimmy 增加多少个气球
    public void AddBalloon(int number)
    {
        for (int i = 0; i < number; i++)
        {
            _balloonName = GenerateBlueName();
            _balloonPath = "Balloon/" + _balloonName;
            GameObject balloon =
                Instantiate(Resources.Load<GameObject>(_balloonPath), gameObject.transform, true);
            balloon.transform.parent = transform.Find("Jimmy").transform;
            if (status == floatingStatus.RIGHT)
            {
                balloon.transform.localPosition = balloonsPositionRight.Get(_balloonName);
            }
            else
            {
                balloon.transform.localPosition = balloonsPositionLeft.Get(_balloonName);
            }

            balloons.Add(balloon);

            Vector3 pos = balloon.transform.position;
            pos.z = 0;
            balloon.transform.position = pos;
            Quaternion rot = balloon.transform.rotation;
            rot.y = 180;
            balloon.transform.rotation = rot;
        }
    }


    // --- 不使用
    // Jimmy 增加一个气球
    public void AddBalloon()
    {
        _balloonName = GenerateBlueName();
        _balloonPath = "balloons/" + _balloonName;
        GameObject balloon =
            Instantiate(Resources.Load<GameObject>(_balloonPath), gameObject.transform, true);
        balloon.transform.position = balloonsPositionLeft.Get(_balloonName);
        balloons.Add(balloon);
    }


    // Jimmy 随机从气球池使用一个气球
    public string GenerateBlueName()
    {
        string[] balloons = balloonPool.ToArray();
        string balloonsName = balloons[Random.Range(0, balloons.Length)];
        balloonPool.Remove(balloonsName);
        return balloonsName;
    }

    // --- 不使用
    // Jimmy 使用一个指定名字的气球
    public void GenerateBlueName(string balloonName)
    {
        balloonPool.Remove(balloonName);
    }

    // Jimmy 归还一个气球进入气球池 —— Destroy 一个气球后，气球归还气球池
    public void ReturnBalloon(string balloonName)
    {
        balloonPool.Add(balloonName);
    }


    // 气球跟随左右移动
    private void NotifyBalloons(floatingStatus status)
    {
        foreach (GameObject balloon in balloons)
        {
            Quaternion rot = balloon.transform.rotation;
            if (status == floatingStatus.LEFT)
            {
                balloon.transform.localPosition = balloonsPositionLeft.Get(balloon.name.Replace("(Clone)", ""));
                rot.y = 0;
                balloon.transform.rotation = rot;
            }
            else
            {
                balloon.transform.localPosition = balloonsPositionRight.Get(balloon.name.Replace("(Clone)", ""));
                rot.y = 180;
                balloon.transform.rotation = rot;
            }
        }
    }

    // Jimmy 丢失一个气球
    private void LostBalloon()
    {
        if (balloons.Count > 0)
        {
            audioSource.Play();
            print("气球破了");
            GameObject balloon = (GameObject)balloons[0];
            balloons.RemoveAt(0);
            string balloonName = balloon.name;
            balloonName = balloonName.Substring(0, balloonName.Length - 7);
            Debug.Log("Return this balloon:" + balloonName);
            ReturnBalloon(balloonName);
            Destroy(balloon);
        }
    }
}