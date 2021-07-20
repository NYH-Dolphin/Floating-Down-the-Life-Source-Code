using System;

public class Collection
{
    private static HashMap<int, String> NameMap = new HashMap<int, string>();
    private static HashMap<int, String> DescriptMap = new HashMap<int, string>();
    
    private int ID;
    private String Name;
    private String Description;
    
    private static int maxCount = 5;

    public static int GetCollection()
    {
        return maxCount;
    }

    public Collection(int id)
    {
        this.ID = id;
        this.Name = NameMap.Get(id);
        this.Description = DescriptMap.Get(id);
    }

    public static void Init()
    {
        InitNameMap();
        InitDescriptMap();
    }

    static void InitNameMap()
    {
        NameMap.Add(0, "coll1");
        NameMap.Add(1, "coll2");
        NameMap.Add(2, "coll3");
        NameMap.Add(3, "coll4");
        NameMap.Add(4, "coll5");
    }

    static void InitDescriptMap()
    {
        DescriptMap.Add(0, "Description1");
        DescriptMap.Add(1, "Description2");
        DescriptMap.Add(2, "Description3");
        DescriptMap.Add(3, "Description4");
        DescriptMap.Add(4, "Description5");
    }

    public String GetName()
    {
        return this.Name;
    }

    public String GetDescription()
    {
        return this.Description;
    }
}