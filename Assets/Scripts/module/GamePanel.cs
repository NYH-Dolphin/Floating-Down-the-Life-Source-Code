using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePanel : BasePanel
{
    private List<GameObject> leftWall = new List<GameObject>();
    private List<GameObject> rightWall = new List<GameObject>();


    private GameObject Jimmy;

    private Mode currentGameMode = Mode.RandomGenerate; // 当前模式 初始状态为随机生成模式
    
    
    private GameObject currentCharacter; // 当前正在谈话的角色
    
    
    enum Mode
    {
        RandomGenerate = 0, // 随机生成模式，面板上物体随机生成 Jimmy 躲避障碍物
        Conversation = 1  // 谈话模式，面板上所有物品暂停，Jimmy状态切换
    }

    //初始化
    public override void OnInit()
    {
        skinPath = "GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        string JimmyPath = "Jimmy/Jimmy";
        Jimmy = Instantiate(Resources.Load<GameObject>(JimmyPath),
            GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
        Jimmy.transform.localPosition = new Vector3(300, 300, 0);

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
            GameObject wall = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wall.GetComponent<RectTransform>().rect.height;
            wall.transform.localPosition = new Vector3(450, end - height / 2, 0);
            end -= height;
            leftWall.Add(wall);
        }

        end = 800;
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
            GameObject wall = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wall.GetComponent<RectTransform>().rect.height;
            wall.transform.localPosition = new Vector3(-450, end - height / 2, 0);
            end -= height;
            rightWall.Add(wall);
        }
        
    }

    private void Update()
    {
        
        switch (currentGameMode)
        {
            case Mode.RandomGenerate:
                RandomGenerateMode();
                CharacterDistanceCheck();
                break;
            case Mode.Conversation:
                ConversationMode();
                break;
        }

    }

    //关闭
    public override void OnClose()
    {
    }



    // 墙壁、障碍物、窗台和角色的随机生成
    void RandomGenerateMode()
    {
     
        // 左墙壁随机生成
        GameObject lastLeft = leftWall.Last();
        if (lastLeft.transform.localPosition.y - lastLeft.GetComponent<RectTransform>().rect.height / 2 >= -805)
        {
            string path = "";
            float random = Random.Range(0f, 1f);
            if (random < 0.4)
                path = "walls/WallS";
            else if (random < 0.8)
                path = "walls/WallM";
            else
                path = "walls/WallL";
            GameObject wall = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wall.GetComponent<RectTransform>().rect.height;
            wall.transform.localPosition = new Vector3(450, -800 - height / 2, 0);
            leftWall.Add(wall);
        }

        // 右墙壁随机生成
        GameObject lastRight = rightWall.Last();
        if (lastRight.transform.localPosition.y - lastRight.GetComponent<RectTransform>().rect.height / 2 >= -805)
        {
            string path = "";
            float random = Random.Range(0f, 1f);
            if (random < 0.4)
                path = "walls/WallS";
            else if (random < 0.8)
                path = "walls/WallM";
            else
                path = "walls/WallL";
            GameObject wall = Instantiate(Resources.Load<GameObject>(path),
                GameObject.Find("Root/Canvas/GamePanel(Clone)").transform, true);
            float height = wall.GetComponent<RectTransform>().rect.height;
            wall.transform.localPosition = new Vector3(-450, -800 - height / 2, 0);
            rightWall.Add(wall);
        }

        // 左墙壁销毁
        GameObject firstLeft = leftWall.First();
        if (firstLeft.transform.localPosition.y - firstLeft.GetComponent<RectTransform>().rect.height / 2 >= 805)
        {
            leftWall.Remove(firstLeft);
            Destroy(firstLeft);
        }

        // 右角色销毁
        GameObject firstRight = rightWall.First();
        if (firstRight.transform.localPosition.y - firstRight.GetComponent<RectTransform>().rect.height / 2 >= 805)
        {
            rightWall.Remove(firstRight);
            Destroy(firstRight);
        }
    }


    // 检查 character list 中，character 与 Jimmy 之间的距离
    void CharacterDistanceCheck()
    {
        for (int i = 0; i < leftWall.Count; i++)
        {
            if (leftWall[i].GetComponent<WallBehavior>().character)
            {
                GameObject character = leftWall[i].GetComponent<WallBehavior>().character;
                // 计算距离
                float distance = character.GetComponent<CharacterBehaviour>()
                    .CalculateDistance(character.transform.position, Jimmy.transform.position);
                // Debug.Log("Find character!!!! The distance is: " + distance);

                // Jimmy 和 Character 在一定的范围内
                if (character.GetComponent<CharacterBehaviour>().InBounds(distance))
                {
                    // 可以触发，且 flowchart 内主动点击角色发生对话事件
                    if (character.GetComponent<CharacterBehaviour>().InConversation())
                    {
                        // Debug.Log("Have conversation!!!");
                        currentCharacter = character; // 设定当前谈话的对象
                        currentGameMode = Mode.Conversation;
                    }
                }
            }
        }

        for (int i = 0; i < rightWall.Count; i++)
        {
            if (rightWall[i].GetComponent<WallBehavior>().character)
            {
                GameObject character = rightWall[i].GetComponent<WallBehavior>().character;
                // 计算距离
                float distance = character.GetComponent<CharacterBehaviour>()
                    .CalculateDistance(character.transform.position, Jimmy.transform.position);
                // Debug.Log("Find character!!!! The distance is: " + distance);
                
                // Jimmy 和 Character 在一定的范围内
                if (character.GetComponent<CharacterBehaviour>().InBounds(distance))
                {
                    if (character.GetComponent<CharacterBehaviour>().InConversation())
                    {
                        // Debug.Log("Have conversation!!!");
                        currentCharacter = character; // 设定当前谈话的对象
                        currentGameMode = Mode.Conversation;
                    }
                }
            }
        }
    }

    
    // 触发对话模式
    void ConversationMode()
    {
        // 存在对话
        if (currentCharacter.GetComponent<CharacterBehaviour>().InConversation())
            WallBehavior.Stop();
        // 结束对话
        else
        {
            WallBehavior.Move();
            currentGameMode = Mode.RandomGenerate;
        }

    }
}