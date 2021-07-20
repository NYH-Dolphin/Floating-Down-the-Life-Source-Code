using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePanel : BasePanel
{
    private List<GameObject> leftWall = new List<GameObject>();
    private List<GameObject> rightWall = new List<GameObject>();
    
    private GameObject Jimmy;
    private GameObject heightUI; // 显示下降高度的UI组件
    private Button stop;

    //初始化
    public override void OnInit()
    {
        skinPath = "GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        stop = skin.transform.Find("stop").GetComponent<Button>();
        stop.onClick.AddListener(OnStopClick);
        
        string JimmyPath = "Jimmy/Jimmy";
        Jimmy = Instantiate(Resources.Load<GameObject>(JimmyPath),
            GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
        Jimmy.transform.localPosition = new Vector3(0, 600, 0);
        
        string LabelPath = "UI/Label";
        heightUI = Instantiate(Resources.Load<GameObject>(LabelPath),
            GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
        heightUI.transform.localPosition = new Vector3(-300, 730, 0);

        float end = 800;
        while (end > -800)
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
            wallL.transform.localPosition = new Vector3(400, end - height / 2, 0);
            wallR.transform.localPosition = new Vector3(-400,end - height / 2, 0);
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
        RandomGenerateMode();
    }

    //关闭
    public override void OnClose()
    {
        leftWall = new List<GameObject>();
        rightWall = new List<GameObject>();
    }
    
    // 墙壁、障碍物、窗台和角色的随机生成
    void RandomGenerateMode()
    {
        // 左墙壁随机生成
        GameObject lastLeft = leftWall.Last();
        if (lastLeft.transform.localPosition.y - lastLeft.GetComponent<RectTransform>().rect.height / 2 >= -815)
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
            Boolean hasObstacle;
            if (Random.Range(0f, 1f) < 0.5)
                hasObstacle = wallL.GetComponent<WallBehavior>().GenerateObstacle();
            else
                hasObstacle = wallR.GetComponent<WallBehavior>().GenerateObstacle();
            if (hasObstacle)
            {
                wallL.GetComponent<WallBehavior>().GenerateCharacter(50);
                wallR.GetComponent<WallBehavior>().GenerateCharacter(50);
            }
            else
            {
                if (Random.Range(0f, 1f) < 0.5)
                    wallL.GetComponent<WallBehavior>().GenerateCharacter(80);
                else
                    wallR.GetComponent<WallBehavior>().GenerateCharacter(80);
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
}