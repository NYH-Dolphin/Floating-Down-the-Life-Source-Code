using System;
using UnityEngine;

public class Collection
{
    private static HashMap<int, String> NameMap = new HashMap<int, string>();
    private static HashMap<int, String> DescriptMap = new HashMap<int, string>();
    
    private int ID;
    private String Name;
    private String Description;
    
    private static int maxCount = 10;

    public static int GetCollectionCount()
    {
        return maxCount;
    }

    public Collection(int id)
    {
        ID = id;
        Name = NameMap.Get(id);
        Description = DescriptMap.Get(id);
    }

    public static void Init()
    {
        InitNameMap();
        InitDescriptMap();
    }

    static void InitNameMap()
    {
        NameMap.Add(0, "计划表");
        NameMap.Add(1, "黑童话集");
        NameMap.Add(2, "九尾猫的铃铛");
        NameMap.Add(3, "电影票");
        NameMap.Add(4, "草莓味的硬糖");
        NameMap.Add(5, "书签");
        NameMap.Add(6, "小杠铃");
        NameMap.Add(7, "完整的心");
        NameMap.Add(8, "小花花");
        NameMap.Add(9, "鸟蛋");
    }

    static void InitDescriptMap()
    {
        DescriptMap.Add(0, "据说会有梦想和目标，才会走的更远");
        DescriptMap.Add(1, "这好像不是我童年看到过的童话故事呢...");
        DescriptMap.Add(2, "跟随了九尾猫多年的铃铛，或许可以实现一个愿望");
        DescriptMap.Add(3, "一张不限场次的电影票，去看看想看的电影吧");
        DescriptMap.Add(4, "甜甜...");
        DescriptMap.Add(5, "正在施工，敬请期待XD");
        DescriptMap.Add(6, "平时要注意锻炼身体！");
        DescriptMap.Add(7, "一个完整的心，两个相互的温暖的人");
        DescriptMap.Add(8, "把它献给你最爱的人吧~");
        DescriptMap.Add(9, "不知道是什么鸟的鸟蛋");
    }

    public String GetName()
    {
        return Name;
    }

    public String GetDescription()
    {
        return Description;
    }

    public static string GetName(int id)
    {
        return NameMap.Get(id);
    }

    public static int GetUnlockNum()
    {
        int unlockNum = 0;
        for (int i = 0; i < maxCount; i++)
        {
            string key = "collection_lock_" + i;
            if (PlayerPrefs.GetInt(key) != 0)
                unlockNum++;
        }
        return unlockNum;
    }
}