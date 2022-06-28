using System;
using System.Collections.Generic;
using UnityEngine;

public class Collection
{
    private static Dictionary<int, string> NameDicCN = new Dictionary<int, string>();
    private static Dictionary<int, string> DescriptDicCN = new Dictionary<int, string>();
    private static Dictionary<int, string> DescriptDicEN = new Dictionary<int, string>();
    private static Dictionary<int, string> NameDicEN = new Dictionary<int, string>();

    private int ID;

    public string Name => PlayerPrefs.GetString("language", "EN") == "CN" ? NameDicCN[ID] : NameDicEN[ID];
    public string Description => PlayerPrefs.GetString("language", "EN") == "CN" ? DescriptDicCN[ID] : DescriptDicEN[ID];

    private static int maxCount = 10;

    public static int GetCollectionCount()
    {
        return maxCount;
    }

    public Collection(int id)
    {
        ID = id;
    }

    public static void Init()
    {
        InitNameMap();
        InitDescriptionMap();
    }

    private static void InitNameMap()
    {
        NameDicCN[0] = "计划表";
        NameDicEN[0] = "Schedule";
        NameDicCN[1] = "黑童话集";
        NameDicEN[1] = "Dark Fairy Tale";
        NameDicCN[2] = "九尾猫的铃铛";
        NameDicEN[2] = "Bell from Nine-tailed Cat";
        NameDicCN[3] = "电影票";
        NameDicEN[3] = "Cinema ticket";
        NameDicCN[4] = "草莓味的硬糖";
        NameDicEN[4] = "Strawberry Candy";
        NameDicCN[5] = "书签";
        NameDicEN[5] = "Bookmark";
        NameDicCN[6] = "小杠铃";
        NameDicEN[6] = "Barbell";
        NameDicCN[7] = "完整的心";
        NameDicEN[7] = "Heart";
        NameDicCN[8] = "小花花";
        NameDicEN[8] = "Flower";
        NameDicCN[9] = "鸟蛋";
        NameDicEN[9] = "Bird Egg";
    }

    private static void InitDescriptionMap()
    {
        DescriptDicCN[0] = "据说会有梦想和目标，才会走的更远";
        DescriptDicEN[0] = "It is said that with dreams and goals can you go further";
        DescriptDicCN[1] = "这好像不是我童年看到过的童话故事呢...";
        DescriptDicEN[1] = "This is not the fairy tale I saw in my childhood...";
        DescriptDicCN[2] = "跟随了九尾猫多年的铃铛，或许可以实现一个愿望";
        DescriptDicEN[2] = "A bells that follows the nine-tailed cats for many years, one wish may come true";
        DescriptDicCN[3] = "一张不限场次的电影票，去看看想看的电影吧";
        DescriptDicEN[3] = "An unlimited ticket to see what you want to see";
        DescriptDicCN[4] = "甜甜...";
        DescriptDicEN[4] = "Sweaty...";
        DescriptDicCN[5] = "好漂亮的书签，别让它落灰了";
        DescriptDicEN[5] = "A a nice bookmark, Don't let it fall to dust!";
        DescriptDicCN[6] = "平时要注意锻炼身体！";
        DescriptDicEN[6] = "Pay attention to exercise!";
        DescriptDicCN[7] = "一个完整的心，两个相互的温暖的人";
        DescriptDicEN[7] = "A complete heart, two mutual warmth of the people";
        DescriptDicCN[8] = "把它献给你最爱的人吧~";
        DescriptDicEN[8] = "Give it to the one you love the most~";
        DescriptDicCN[9] = "不知道是什么鸟的鸟蛋";
        DescriptDicEN[9] = "Why there is a bird egg???";
    }

    public static string GetName(int id)
    {
        return PlayerPrefs.GetString("language", "EN") == "CN" ? NameDicCN[id] : NameDicEN[id];
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