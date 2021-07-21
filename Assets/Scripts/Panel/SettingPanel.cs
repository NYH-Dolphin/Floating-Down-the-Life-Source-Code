using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{

    private Button close;

    public static Collection[] AllCollections;




    //初始化
    public override void OnInit()
    {
        skinPath = "CollectionPanel";
        layer = PanelManager.Layer.Panel;
        
    }

    //显示
    public override void OnShow(params object[] args)
    {

        close = skin.transform.Find("close").GetComponent<Button>();
        
        close.onClick.AddListener(OnCloseClick);
        showPage();
    }

    //关闭
    public override void OnClose()
    {
		
    }

    private void showPage()
    {
        
        int count = 0;
        for (int i = 475; i >= -200; i -= 225)
        {
            for (int j = -200; j <= 200; j += 200)
            {
                GameObject grid = Instantiate(Resources.Load<GameObject>("collection/grid"),
                    GameObject.Find("Root/Canvas/CollectionPanel(Clone)").transform, true);
                count++;
            }
        }
    }
    
    private void OnLeftClick()
    {
        
    }
	
    private void OnRightClick()
    {
       
    }

    private void OnCloseClick()
    {
        Close();
    }

    
}