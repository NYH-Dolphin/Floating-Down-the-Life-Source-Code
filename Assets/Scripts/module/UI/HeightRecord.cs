using System;
using TMPro;
using UnityEngine;

public class HeightRecord : MonoBehaviour
{

    private float height = 0; // 记录目前的高度
    public GameObject heightRecord; // 字符
    private static bool pause = false;

    void Start()
    {
        heightRecord.GetComponent<TMP_Text>().text = height + " m";
    }

    void Update()
    {
        if (!pause)
        {
            height += Time.deltaTime;
            heightRecord.GetComponent<TMP_Text>().text = (int) height + " m";
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

    
    public int GetHeight()
    {
        return (int)height;
    }


}