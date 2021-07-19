using UnityEngine.UI;

public class CollectionPanel : BasePanel
{
    
    //初始化
    public override void OnInit()
    {
        skinPath = "CollectionPanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        
    }

    //关闭
    public override void OnClose()
    {
		
    }
	
    //当按下开始按钮
    public void OnBeginClick()
    {
        
    }
	
    public void OnRulesClick()
    {

    }
}