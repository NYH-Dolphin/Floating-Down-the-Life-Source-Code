using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private float speed = 100f;

    private readonly string[] _smallObstacles = {"obstacle_2", "obstacle_6", "obstacle_9"};
    private readonly string[] _midObstacles = {"obstacle_1", "obstacle_3", "obstacle_4", "obstacle_5"};
    private readonly string[] _largeObstacles = {"obstacle_7", "obstacle_8"};

    private string[] WindowStillsName = {"window1", "window2", "window3"};

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
                    // 40% 的概率生成一个 window
                    if (RandomGenerate(40))
                    {
                        string windowName = WindowStillsName[Random.Range(0, WindowStillsName.Length)];
                        string windowPath = "window/left/" + windowName;
                        GameObject windowL = Instantiate(Resources.Load<GameObject>(windowPath),
                            transform, true);
                        windowL.transform.localPosition = new Vector3(375, 0, 0);
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
                        string windowName = WindowStillsName[Random.Range(0, WindowStillsName.Length)];
                        string windowPath = "window/right/" + windowName;
                        GameObject windowR = Instantiate(Resources.Load<GameObject>(windowPath),
                            transform, true);

                        windowR.transform.localPosition = new Vector3(-375, 0, 0);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        p += (speed * Time.smoothDeltaTime) * new Vector3(0, 1, 0);
        transform.localPosition = p;
    }

    /**
     * 指定概率的生成
     */
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
}