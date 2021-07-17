using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private float speed = 100f;

    private readonly string[] _smallObstacles = {"obstacle_2", "obstacle_6", "obstacle_9"};
    private readonly string[] _midObstacles = {"obstacle_1", "obstacle_3", "obstacle_4", "obstacle_5"};
    private readonly string[] _largeObstacles = {"obstacle_7", "obstacle_8"};
    private readonly string[] _windowStillsName = {"window1", "window2", "window3"};
    private readonly string[] _characterName = {"character1", "character2", "character3", "character4"};


    private static bool stop = false; // 如果有 conversation 停止
    private GameObject window;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        if (RandomGenerate(40))
        {
            Vector3 p = transform.position;
            if (p.x < 0)
            {
                string obsName = "";
                if (GetComponent<RectTransform>().rect.height < 300)
                    obsName = _smallObstacles[Random.Range(0, _smallObstacles.Length)];
                else if (GetComponent<RectTransform>().rect.height < 500)
                    obsName = _midObstacles[Random.Range(0, _midObstacles.Length)];
                else
                {
                    obsName = _largeObstacles[Random.Range(0, _largeObstacles.Length)];
                }

                string path = "obstacles/left/" + obsName;
                GameObject obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
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
                GameObject obstacle = Instantiate(Resources.Load<GameObject>(path), transform, true);
                float halfWidth = obstacle.GetComponent<RectTransform>().rect.width / 2;
                obstacle.transform.localPosition = new Vector3(-125 - halfWidth, 0, 0);
            }
        }
        else
        {
            Vector3 p = transform.position;
            if (p.x < 0)
            {
                if (GetComponent<RectTransform>().rect.height >= 300)
                {
                    // 40% 的概率生成一个 window 和 character
                    if (RandomGenerate(40))
                    {
                        // 角色
                        string characterName = _characterName[Random.Range(0, _characterName.Length)];
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
                }
            }
            else
            {
                if (GetComponent<RectTransform>().rect.height >= 300)
                {

                    // 40% 的概率生成一个 window
                    if (RandomGenerate(40))
                    {
                        string windowName = _windowStillsName[Random.Range(0, _windowStillsName.Length)];
                        string windowPath = "window/right/" + windowName;
                        window = Instantiate(Resources.Load<GameObject>(windowPath),
                            transform, true);

                        window.transform.localPosition = new Vector3(-375, 0, 0);
                    }
                }
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
    
}