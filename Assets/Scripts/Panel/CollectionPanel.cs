using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel : BasePanel
{
    private int Page = 0;

    private Button left;
    private Button right;
    private Button close;

    private List<GameObject> girds = new List<GameObject>();
    
    private int MaxPage;
    public static Collection[] AllCollections;
    private static int[] Unlocked;

    private static GridBehavior[] observerList;
    
    
    //初始化
    public override void OnInit()
    {
        skinPath = "CollectionPanel";
        layer = PanelManager.Layer.Panel;

        MaxPage = Collection.GetCollection() / 12;
        if (Collection.GetCollection() % 12 != 0)
            MaxPage++;
        AllCollections = new Collection[MaxPage * 12];
        Unlocked = new int[MaxPage * 12];
        observerList = new GridBehavior[MaxPage * 12];

        for (int i = 0; i < MaxPage; i++)
        {
            AllCollections[i] = new Collection(i);
        }
        
        Unlocked[2] = 1;
        Unlocked[0] = 1;
        Unlocked[3] = 4;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        left = skin.transform.Find("left").GetComponent<Button>();
        right = skin.transform.Find("right").GetComponent<Button>();
        close = skin.transform.Find("close").GetComponent<Button>();
        
        left.onClick.AddListener(OnLeftClick);
        right.onClick.AddListener(OnRightClick);
        close.onClick.AddListener(OnCloseClick);
        showPage();
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void showPage()
    {
        foreach (var gird in girds)
            Destroy(gird);
        int count = 0;
        for (int i = 475; i >= -200; i -= 225)
        {
            for (int j = -200; j <= 200; j += 200)
            {
                GameObject grid = Instantiate(Resources.Load<GameObject>("collection/grid"),
                    GameObject.Find("Root/Canvas/CollectionPanel(Clone)").transform, true);
                grid.transform.localPosition = new Vector3(j, i, 0);
                grid.GetComponent<GridBehavior>().Init(Page * 12 + count, Unlocked[Page*12+count]==0);
                observerList[Page * 12 + count] = grid.GetComponent<GridBehavior>();
                girds.Add(grid);
                count++;
            }
        }
    }
    
    private void OnLeftClick()
    {
        if (Page > 0)
        {
            Page--;
            showPage();
        }
    }
	
    private void OnRightClick()
    {
        if (Page < MaxPage - 1)
        {
            Page++;
            showPage();
        }
    }

    private void OnCloseClick()
    {
        Close();
    }

    public static void UnlockCollection(int id)
    {
        Unlocked[id]++;
        observerList[id].SetIconVisibility(Unlocked[id]==0);
    }
}