using System;

public class Collection
{
    private static HashMap<int, String> NameMap = new HashMap<int, string>();
    private static HashMap<int, String> DescriptMap = new HashMap<int, string>();
    
    private int ID;
    private String Name;
    private String Description;
    
    private static int maxCount = 6;

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
        NameMap.Add(4, "coll5");
        NameMap.Add(5, "书签");
    }

    static void InitDescriptMap()
    {
        DescriptMap.Add(0, "据说会有梦想和目标，才会走的更远");
        DescriptMap.Add(1, "这好像不是我童年看到过的童话故事呢...");
        DescriptMap.Add(2, "Description3");
        DescriptMap.Add(3, "一张不限场次的电影票，去看看想看的电影吧");
        DescriptMap.Add(4, "Description5");
        DescriptMap.Add(5, "正在施工，敬请期待XD");
    }

    public String GetName()
    {
        return Name;
    }

    public String GetDescription()
    {
        return Description;
    }
}