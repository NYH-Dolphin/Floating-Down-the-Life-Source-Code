using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallBehavior : MonoBehaviour
{
    private static float speed = 200f;

    private readonly string[] _smallObstacles = {"obstacle_2", "obstacle_6", "obstacle_10", "obstacle_12", "obstacle_19", "obstacle_20"};
    private readonly string[] _midObstacles = {"obstacle_1", "obstacle_3", "obstacle_15", "obstacle_17", "obstacle_21"};
    private readonly string[] _largeObstacles = {"obstacle_4", "obstacle_5", "obstacle_7", "obstacle_8", "obstacle_9", "obstacle_11", "obstacle_13", "obstacle_14", "obstacle_16", "obstacle_18"};
    private readonly string[] _windowStillsName = {"window1", "window2", "window3"};
    private readonly string[] _leftCharacterName = {"character1", "character4","character5",
        "character6","character7"};
    private readonly string[] _rightCharacterName = {"character3", "character12"};

    private GameObject obstacle = null;
    private int obstaclePossibility = 40;

    private static bool stop = false; // 如果有 conversation 停止
    private GameObject window;
    public GameObject character;

    private GameObject LabelUI; // 找到高度记录时间

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Boolean GenerateObstacle()
    {
        // 生成障碍物的概率随时间而增加
        LabelUI = transform.parent.Find("Label(Clone)").gameObject;
        obstaclePossibility = 40 + LabelUI.GetComponent<HeightRecord>().GetHeight() / 4;
        if (obstaclePossibility > 100)
            obstaclePossibility = 100;

        if (RandomGenerate(obstaclePossibility))
        {
            Vector3 p = transform.localPosition;
            if (p.x < 0)
            {
                string obsName = "";
                if (GetComponent<RectTransform>().rect.height < 300)
                    obsName = _smallObstacles[Random.Range(0, _smallObstacles.Length)];
                else if (GetComponent<RectTransform>().rect.height < 500)
                    obsName = _midObstacles[Random.Range(0, _midObstacles.Length)];
                else
                    obsName = _largeObstacles[Random.Range(0, _largeObstacles.Length)];

                string path = "obstacles/left/" + obsName;
                obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
                float halfWidth = obstacle.GetComponent<RectTransform>().rect.width / 2;
                obstacle.transform.localPosition = new Vector3(125 + halfWidth, 0, 0);
            }
            else
            {
                string obsName = "";
                if (GetComponent<RectTransform>().rect.height < 300)
                    obsName = _smallObstacles[Random.Range(0, _smallObstacles.Length)];
                else if (GetComponent<RectTransform>().rect.height < 500)
                    obsName = _midObstacles[Random.Range(0, _midObstacles.Length)];
                else
                    obsName = _largeObstacles[Random.Range(0, _largeObstacles.Length)];
                string path = "obstacles/right/" + obsName;
                obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
                float halfWidth = obstacle.GetComponent<RectTransform>().rect.width / 2;
                obstacle.transform.localPosition = new Vector3(-125 - halfWidth, 0, 0);
            }

            return true;
        }

        return false;
    }

    public void GenerateCharacter(int random)
    {
        if (obstacle != null)
            return;
        Vector3 p = transform.localPosition;
        float height = GetComponent<RectTransform>().rect.height;
        if (height > 300 && RandomGenerate(random))
        {
            if (p.x < 0)
            {
                // 角色
                string characterName = _leftCharacterName[Random.Range(0, _leftCharacterName.Length)];
                string characterPath = "character/left/" + characterName;
                character = Instantiate(Resources.Load<GameObject>(characterPath),
                    transform, true);
                character.transform.localPosition = new Vector3(200, 0, 0);

                // 窗台
                string windowName = _windowStillsName[Random.Range(0, _windowStillsName.Length)];
                string windowPath = "window/left/" + windowName;
                window = Instantiate(Resources.Load<GameObject>(windowPath),
                    transform, true);
                window.transform.localPosition = new Vector3(375, 0, 0);
                window.gameObject.layer = 2;
            }
            else
            {
                // 角色
                string characterName = _rightCharacterName[Random.Range(0, _rightCharacterName.Length)];
                string characterPath = "character/right/" + characterName;
                character = Instantiate(Resources.Load<GameObject>(characterPath),
                    transform, true);
                character.transform.localPosition = new Vector3(-200, 0, 0);
                        
                // 窗台
                string windowName = _windowStillsName[Random.Range(0, _windowStillsName.Length)];
                string windowPath = "window/right/" + windowName;
                window = Instantiate(Resources.Load<GameObject>(windowPath),
                    transform, true);
                window.transform.localPosition = new Vector3(-375, 0, 0);
                window.gameObject.layer = 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 继续运动
        if (!stop)
        {
            Vector3 p = transform.localPosition;
            p += (speed * Time.smoothDeltaTime) * new Vector3(0, 1, 0);
            transform.localPosition = p;
        }
    }

    // 指定概率返回 true
    private bool RandomGenerate(int percentage)
    {
        // 随机生成 [1,100]之间的数
        int num = Random.Range(1, 101);
        if (num <= percentage)
        {
            return true;
        }
        return false;
    }


    // 被 GamePanel 调用，停止运动
    // conversation mode
    public static void Stop()
    {
        stop = true;
    }


    // 被 GamePanel 调用，继续运动
    // conversation mode
    public static void Move()
    {
        stop = false;
    }

    public static void SetSpeed(float speed)
    {
        WallBehavior.speed = speed;
    }

    public static float GetSpeed()
    {
        return WallBehavior.speed;
    }
    
}