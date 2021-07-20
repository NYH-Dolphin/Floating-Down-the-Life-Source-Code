using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel : BasePanel
{
    private int Page = 1;

    private Button left;
    private Button right;
    private Button close;

    private List<GameObject> girds = new List<GameObject>();
    
    public static int COLLECTIONCNT;
    public static Collection[] AllCollections;
    private static int[] Unlocked;

    private static GridBehavior[] observerList;
    
    
    //初始化
    public override void OnInit()
    {
        skinPath = "CollectionPanel";
        layer = PanelManager.Layer.Panel;
        
        COLLECTIONCNT = 12 * Page;
        AllCollections = new Collection[COLLECTIONCNT];
        Unlocked = new int[COLLECTIONCNT];
        observerList = new GridBehavior[COLLECTIONCNT];

        for (int i = 0; i < COLLECTIONCNT; i++)
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
                grid.GetComponent<GridBehavior>().Init(Page * 12 + count);
                girds.Add(grid);
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
        if (Page < 2)
        {
            Page++;
            showPage();
        }
    }

    private void OnCloseClick()
    {
        Close();
    }
}