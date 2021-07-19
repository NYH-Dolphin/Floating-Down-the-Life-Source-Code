using System;

public class Collection
{
    private static HashMap<int, String> NameMap = new HashMap<int, string>();
    private static HashMap<int, String> DescriptMap = new HashMap<int, string>();
    
    private int ID;
    private String Name;
    private String Description;
    
    private static int maxCount;

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
        NameMap.Add(0,"balloon");
        
    }

    static void InitDescriptMap()
    {
        DescriptMap.Add(0,"balloon");
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