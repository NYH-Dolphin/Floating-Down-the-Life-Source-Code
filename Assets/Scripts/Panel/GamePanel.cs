using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePanel : BasePanel
{
    private List<GameObject> leftWall = new List<GameObject>();
    private List<GameObject> rightWall = new List<GameObject>();

    // // 天气面板
    private SpriteRenderer morning;
    private SpriteRenderer noon;
    private SpriteRenderer night;
    private List<SpriteRenderer> background = new List<SpriteRenderer>();
    private int showIndex;
    private Flowchart backgroundFlowChart;

    private GameObject Jimmy;
    private GameObject heightUI; // 显示下降高度的UI组件
    private Button stop;

    private bool ending = false; // 是否是结局
    private float endingTime = 0f; // 墙壁还能再往上的时间 5s

    //初始化
    public override void OnInit()
    {
        skinPath = "GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        // 暂停 Button
        stop = skin.transform.Find("stop").GetComponent<Button>();
        stop.onClick.AddListener(OnStopClick);

        // Jimmy
        string JimmyPath = "Jimmy/Jimmy";
        Jimmy = Instantiate(Resources.Load<GameObject>(JimmyPath),
            GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
        Jimmy.transform.localPosition = new Vector3(0, 600, 0);

        // 下降高度 Label
        string LabelPath = "UI/Label";
        heightUI = Instantiate(Resources.Load<GameObject>(LabelPath),
            GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
        heightUI.transform.localPosition = new Vector3(-300, 730, 0);

        // // 背景板初始化
        morning = skin.transform.Find("morning").GetComponent<SpriteRenderer>();
        noon = skin.transform.Find("noon").GetComponent<SpriteRenderer>();
        night = skin.transform.Find("night").GetComponent<SpriteRenderer>();
        backgroundFlowChart = skin.transform.Find("Flowchart").GetComponent<Flowchart>();
        background.Add(morning);
        background.Add(noon);
        background.Add(night);
        showIndex = 0;
        backgroundFlowChart.SetIntegerVariable("background", showIndex);
        for (int i = 0; i < background.Count; i++)
        {
            if (i != showIndex)
            {
                Color thisColor = background[i].color;
                background[i].color = new Color(thisColor.r, thisColor.g, thisColor.b, 0);
            }
            else
            {
                Color thisColor = background[i].color;
                background[i].color = new Color(thisColor.r, thisColor.g, thisColor.b, 1);
            }
        }

        // 墙壁持续生成
        float end = 800;
        while (end > -800)
        {
            string path = "";
            float random = Random.Range(0f, 1f);
            if (random < 0.3)
                path = "walls/WallS";
            else if (random < 0.6)
                path = "walls/WallM";
            else
                path = "walls/WallL";
            GameObject wallL = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            GameObject wallR = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wallL.GetComponent<RectTransform>().rect.height;
            wallL.transform.localPosition = new Vector3(400, end - height / 2, 0);
            wallR.transform.localPosition = new Vector3(-400, end - height / 2, 0);
            leftWall.Add(wallL);
            rightWall.Add(wallR);
            wallL.transform.SetAsFirstSibling();
            wallR.transform.SetAsFirstSibling();
            end -= height;
        }
    }

    public void OnStopClick()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            Jimmy.GetComponent<SpriteRenderer>().sortingOrder = -1;
            foreach (GameObject balloon in Jimmy.GetComponent<JimmyBehaviour>().balloons)
                balloon.GetComponent<SpriteRenderer>().sortingOrder = -1;
            PanelManager.Open<StopPanel>();
        }
        else
        {
            Time.timeScale = 1;
            Jimmy.GetComponent<SpriteRenderer>().sortingOrder = 0;
            foreach (GameObject balloon in Jimmy.GetComponent<JimmyBehaviour>().balloons)
                balloon.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    private void Update()
    {
        if (Jimmy.transform.localPosition.y >= 100)
            Jimmy.transform.localPosition += (300 * Time.smoothDeltaTime) * new Vector3(0, -1, 0);

        if (!HeightRecord.IsEnd())
        {
            RandomGenerateMode();
        }
        else
        {
            EndingMode();
        }
    }

    //关闭
    public override void OnClose()
    {
        leftWall = new List<GameObject>();
        rightWall = new List<GameObject>();
    }

    // 墙壁、障碍物、窗台和角色的随机生成
    private bool leftHasCharacter; // 记录上一个墙是不是有角色生成
    private bool rightHasCharacter; // 记录上一个墙是不是有角色生成
    void RandomGenerateMode()
    {
        // 随机生成
        GameObject lastLeft = leftWall.Last();
        if (lastLeft.transform.localPosition.y - lastLeft.GetComponent<RectTransform>().rect.height / 2 >= -845)
        {
            string path = "";
            float random = Random.Range(0f, 1f);
            if (random < 0.4)
                path = "walls/WallS";
            else if (random < 0.8)
                path = "walls/WallM";
            else
                path = "walls/WallL";
            GameObject wallL = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            GameObject wallR = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wallL.GetComponent<RectTransform>().rect.height;
            wallL.transform.localPosition = new Vector3(400, -800 - height / 2, 0);
            wallR.transform.localPosition = new Vector3(-400, -800 - height / 2, 0);
            leftWall.Add(wallL);
            rightWall.Add(wallR);
            wallL.transform.SetAsFirstSibling();
            wallR.transform.SetAsFirstSibling();
            bool hasObstacle;
            if (Random.Range(0f, 1f) < 0.5)
            {
                if (!leftHasCharacter)
                {
                    hasObstacle = wallL.GetComponent<WallBehavior>().GenerateObstacle();
                }
                else
                {
                    hasObstacle = false;
                    leftHasCharacter = false;
                }
            }
            else
            {
                if (!rightHasCharacter)
                {
                    hasObstacle = wallR.GetComponent<WallBehavior>().GenerateObstacle();
                }
                else
                {
                    hasObstacle = false;
                    rightHasCharacter = false;
                }
            }
            if (hasObstacle)
            {
                leftHasCharacter = wallL.GetComponent<WallBehavior>().GenerateCharacter(50);
                rightHasCharacter = wallR.GetComponent<WallBehavior>().GenerateCharacter(50);
            }
            else
            {
                if (Random.Range(0f, 1f) < 0.5)
                    leftHasCharacter = wallL.GetComponent<WallBehavior>().GenerateCharacter(80);
                else
                    rightHasCharacter = wallR.GetComponent<WallBehavior>().GenerateCharacter(80);
            }
        }

        // 左墙壁销毁
        GameObject firstLeft = leftWall.First();
        if (firstLeft.transform.localPosition.y - firstLeft.GetComponent<RectTransform>().rect.height / 2 >= 815)
        {
            leftWall.Remove(firstLeft);
            Destroy(firstLeft);
        }

        // 右墙壁销毁
        GameObject firstRight = rightWall.First();
        if (firstRight.transform.localPosition.y - firstRight.GetComponent<RectTransform>().rect.height / 2 >= 815)
        {
            rightWall.Remove(firstRight);
            Destroy(firstRight);
        }
    }

    

    private bool hasGenerate = false;

    // 结局地面生成
    void EndingMode()
    {
        endingTime += Time.deltaTime;
        if (endingTime <= 2)
        {
            EndingGenerateMode();
        }
        else
        {
            if (!hasGenerate)
            {
                if (leftWall.Last().GetComponent<RectTransform>().localPosition.y >= -650)
                {
                    leftWall.Last().GetComponent<WallBehavior>().GenerateGround();
                    hasGenerate = true;
                }
            }

            if (leftWall.Last().GetComponent<RectTransform>().localPosition.y >= -550)
            {
                Jimmy.GetComponent<JimmyBehaviour>().FallIntoTheGround();
                WallBehavior.End();
            }
        }
    }


    void EndingGenerateMode()
    {
        // 左墙壁随机生成
        GameObject lastLeft = leftWall.Last();
        if (lastLeft.transform.localPosition.y - lastLeft.GetComponent<RectTransform>().rect.height / 2 >= -815)
        {
            string path = "walls/WallL";
            GameObject wallL = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            GameObject wallR = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wallL.GetComponent<RectTransform>().rect.height;
            wallL.transform.localPosition = new Vector3(400, -800 - height / 2, 0);
            wallR.transform.localPosition = new Vector3(-400, -800 - height / 2, 0);
            Debug.Log(endingTime);
            leftWall.Add(wallL);
            rightWall.Add(wallR);
            wallL.transform.SetAsFirstSibling();
            wallR.transform.SetAsFirstSibling();
        }

        // 左墙壁销毁
        GameObject firstLeft = leftWall.First();
        if (firstLeft.transform.localPosition.y - firstLeft.GetComponent<RectTransform>().rect.height / 2 >= 815)
        {
            leftWall.Remove(firstLeft);
            Destroy(firstLeft);
        }

        // 右墙壁销毁
        GameObject firstRight = rightWall.First();
        if (firstRight.transform.localPosition.y - firstRight.GetComponent<RectTransform>().rect.height / 2 >= 815)
        {
            rightWall.Remove(firstRight);
            Destroy(firstRight);
        }
    }
}