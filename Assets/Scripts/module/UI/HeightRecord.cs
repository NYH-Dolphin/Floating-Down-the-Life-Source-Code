using System;
using TMPro;
using UnityEngine;

public class HeightRecord : MonoBehaviour
{
    
    public GameObject heightRecord; // 字符
    private static bool pause = false; // 仅是暂停
    private static bool end = false;   // 结束记录
    private static float height = 0; // 记录目前的高度
    // private static float totalHeight = 0; // 记录游戏的高度加总 —— 标识着游戏的结束
    // 结束的米数记录
    private const int endMeter = 600;

    void Start()
    {
        heightRecord.GetComponent<TMP_Text>().text = height + " m";
    }

    void Update()
    {
        if (!end)
        {
            if (!pause)
            {
                CheckEnding();
                height += Time.deltaTime * (WallBehavior.GetSpeed() / 200);
                float totalHeight = PlayerPrefs.GetFloat("totalHeight") +
                                    Time.deltaTime * (WallBehavior.GetSpeed() / 200);
                PlayerPrefs.SetFloat("totalHeight", totalHeight);
                heightRecord.GetComponent<TMP_Text>().text = (int) height + " m";
            }
        }
    }


    // 暂停
    public static void Pause()
    {
        pause = true;
    }

    // 继续记录高度
    public static void Continue()
    {
        pause = false;
    }
    
    
    // 开始
    public static void Begin()
    {
        end = false;
    }

    // 结束
    public static void End()
    {
        end = true;
    }

    
    // GamePanel 调用判断是否游戏结束
    public static bool IsEnd()
    {
        return end;
    }

    public static int GetHeight()
    {
        return (int)height;
    }
    
    // Start Panel 每次刷新height
    public static void RefreshHeight()
    {
        height = 0;
    }


    private static int GetTotalHeight()
    {
        return (int) PlayerPrefs.GetFloat("totalHeight");
    }


    private static void RefreshTotalHeight()
    {
        PlayerPrefs.SetFloat("totalHeight", 0);
    }
    
    
    // 检查游戏是否到了结局
    private static void CheckEnding()
    {
        if (GetTotalHeight() == endMeter)
        {
            GameObject.Find("Audio Source").GetComponent<MusicController>().PlayEndMusic();
            End();// 终止高度记录
            RefreshTotalHeight(); // totalHeight 清零
        }
    }

}