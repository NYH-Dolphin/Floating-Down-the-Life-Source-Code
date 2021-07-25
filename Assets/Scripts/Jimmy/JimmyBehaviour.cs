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

    public Boolean isCollide = false; // 判断是否碰撞
    private const int InvalidTimeTick = 300; // 无敌时间
    private int invalidTimeCount = 0; // 无敌时间记数
    private Renderer jimmyRenderer = null; // Jimmy 的图片

    private static bool stop = false; // 控制 Jimmy 是否能移动


    // 气球的名字和可以使用的气球
    private static readonly string[] BalloonsName =
    {
        "blue_balloon", "cyan_balloon",
        "grassgreen_balloon", "green_balloon", "orange_balloon", "pink_balloon",
        "purple_balloon", "red_balloon", "skyblue_balloon", "yellow_balloon", "gray_balloon"
    };

    // 记录气球和对应的气球的 localPosition
    private static HashMap<string, Vector3> balloonsPosition = new HashMap<string, Vector3>();

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


    private const float DASHSPEED = 500f;
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
                GameObject balloon = (GameObject) balloons[i];
                balloon.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            jimmyRenderer.enabled = true;
            for (int i = 0; i < balloons.Count; i++)
            {
                GameObject balloon = (GameObject) balloons[i];
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
        stop = true;
    }

    // Jimmy 继续移动
    public static void Continue()
    {
        stop = false;
    }


    // Jimmy 的移动
    private const float MAXFLOATSPEED = 500f;
    private const float MINFLOATSPEED = 100f;
    private float floatSpeed = 100f;

    void Float()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 p = transform.position;
        // 向左移动
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Debug.Log("LEF
            //  T");
            floatSpeed = floatSpeed <= MINFLOATSPEED ? MINFLOATSPEED : floatSpeed;
            floatSpeed += Time.deltaTime * 700;
            floatSpeed = floatSpeed >= MAXFLOATSPEED ? MAXFLOATSPEED : floatSpeed;
            // 位置移动
            p.x += moveX * floatSpeed * Time.deltaTime;
            // Animation Controller 的参数状态更新
            animator.SetBool("floatLeft", true);

            if (status != floatingStatus.LEFT)
            {
                status = floatingStatus.LEFT;
                NotifyBalloons();
            }
        }
        // 向右移动
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Debug.Log("RIGHT");
            floatSpeed = floatSpeed <= MINFLOATSPEED ? MINFLOATSPEED : floatSpeed;
            floatSpeed += Time.deltaTime * 700;
            floatSpeed = floatSpeed >= MAXFLOATSPEED ? MAXFLOATSPEED : floatSpeed;
            p.x += moveX * floatSpeed * Time.deltaTime;
            animator.SetBool("floatLeft", false);

            if (status != floatingStatus.RIGHT)
            {
                status = floatingStatus.RIGHT;
                NotifyBalloons();
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
            // Time.timeScale. = 0;
            // GetComponent<SpriteRenderer>().sortingOrder = -1;
            // foreach (GameObject balloon in GetComponent<JimmyBehaviour>().balloons)
            //     balloon.GetComponent<SpriteRenderer>().sortingOrder = -1;
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

        balloonsPosition.Add("gray_balloon", new Vector3(0.8f, -1.03f, 0f));
        balloonsPosition.Add("red_balloon", new Vector3(1.25f, 2.58f, 0f));
        balloonsPosition.Add("orange_balloon", new Vector3(0.67f, 1.13f, 0f));
        balloonsPosition.Add("pink_balloon", new Vector3(1.17f, 2.36f, 0f));
        balloonsPosition.Add("cyan_balloon", new Vector3(1f, 3f, 0f)); // ?????
        balloonsPosition.Add("yellow_balloon", new Vector3(1.17f, 2.97f, 0f));
        balloonsPosition.Add("blue_balloon", new Vector3(1.33f, 3f, 0f));
        balloonsPosition.Add("purple_balloon", new Vector3(1.29f, 2.96f, 0f));
        balloonsPosition.Add("grassgreen_balloon", new Vector3(0.8f, 1f, 0f));
        balloonsPosition.Add("green_balloon", new Vector3(2f, 3.2f, 0f));
        balloonsPosition.Add("skyblue_balloon", new Vector3(1.27f, 2.57f, 0f));
    }


    // 指定 Jimmy 增加多少个气球
    public void AddBalloon(int number)
    {
        for (int i = 0; i < number; i++)
        {
            _balloonName = GenerateBlueName();
            _balloonPath = "balloons/" + _balloonName;
            GameObject balloon =
                Instantiate(Resources.Load<GameObject>(_balloonPath), gameObject.transform, true);
            balloon.transform.localPosition = balloonsPosition.Get(_balloonName);
            if (status == floatingStatus.RIGHT)
            {
                balloon.transform.RotateAround(transform.position, transform.up, 180f);
            }

            balloons.Add(balloon);
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
        balloon.transform.localPosition = balloonsPosition.Get(_balloonName);
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
    private void NotifyBalloons()
    {
        foreach (GameObject balloon in balloons)
        {
            balloon.transform.RotateAround(transform.position, transform.up, 180f);
        }
    }

    // Jimmy 丢失一个气球
    private void LostBalloon()
    {
        if (balloons.Count > 0)
        {
            audioSource.Play();
            print("气球破了");
            GameObject balloon = (GameObject) balloons[0];
            balloons.RemoveAt(0);
            string balloonName = balloon.name;
            balloonName = balloonName.Substring(0, balloonName.Length - 7);
            Debug.Log("Return this balloon:" + balloonName);
            ReturnBalloon(balloonName);
            Destroy(balloon);
        }
    }
}