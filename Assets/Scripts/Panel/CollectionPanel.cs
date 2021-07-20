using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel : BasePanel
{
    private int Page = 1;
    
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
        Unlocked[2] = 1;
        Unlocked[0] = 1;
        Unlocked[3] = 4;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        showPage();
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void showPage()
    {
        int itemID = 0;
        for (int i = 0; i < Page; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    GameObject grid = Instantiate(Resources.Load<GameObject>("collection/grid"),
                        GameObject.Find("Root/Canvas/CollectionPanel(Clone)").transform, true);
                    grid.transform.localPosition = new Vector3(-200+k*200,475-j*225, 0);
                    
                    grid.GetComponent<GridBehavior>().Init(itemID,Unlocked[itemID]==0);
                    observerList[itemID] = grid.GetComponent<GridBehavior>();
                    
                    itemID++;

                }
                
            }
            
        }
    }

    public static void UnlockCollection(int id)
    {
        Unlocked[id]++;
        observerList[id].SetIconVisibility(Unlocked[id]==0);
    }

    


}